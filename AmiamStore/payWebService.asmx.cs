using AmiamStore.PaymentApp_DAL;
using AmiamStore.PaymentApp_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Net.Mail;
using AmiamStore.Models;
using System.ComponentModel;

namespace AmiamStore
{
    /// <summary>
    /// Summary description for payWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class payWebService : System.Web.Services.WebService
    {
            public static PaymentManager _paymentManager = new PaymentManager();

        CreditCardRepository DAL = new CreditCardRepository();

        [WebMethod]
            public bool ConfirmPay(string holderName, string creditCardNumber, string cvv, string expirityDate, double amountToCharge,string EmailTo, List<CartModel> products)
            {
                if (ProperCardDetails(holderName, creditCardNumber, cvv, expirityDate, amountToCharge) && ClientHasEnoghMoney(holderName,amountToCharge,cvv,expirityDate,creditCardNumber))
                {
                    amountToCharge = DiscountForMasterCardClients(creditCardNumber, amountToCharge);
                    DAL.UpdateLine(amountToCharge, holderName, creditCardNumber, cvv, expirityDate);
                    SendPurchaseClearanceOnEmail(EmailTo , creditCardNumber , products);
                    return true;
                }
                return false;
            }



            [WebMethod]
            public bool ProperCardDetails(string holderName, string creditCardNumber, string cvv, string expirityDate, double amountToCharge)
            {
                string month = expirityDate.Substring(0, 2);
                string year = expirityDate.Substring(4, 3);
                if (cvv.Length == 3 && creditCardNumber.Length == 16 || creditCardNumber.Length == 15 && int.Parse(month) > 0 && int.Parse(month) <= 12 && int.Parse(year) > 20)
                    return true;
                return false;
            }
            [WebMethod]
            public double DiscountForMasterCardClients(string creditCardNumber, double amountToPay)
            {
                if (_paymentManager.IsMasterCardHolder(creditCardNumber))
                {
                    double UpdatedAmountToPay = ((amountToPay * 10) / 100);
                    return (amountToPay - UpdatedAmountToPay);
                }
                else
                    return amountToPay;
            }
            [WebMethod]
            public bool ClientHasEnoghMoney(string holderName, double amountToPay , string cvv , string expirty , string creditCardNumber)
            {
            CreditCardRepository Repository = new CreditCardRepository();
            if (!_paymentManager.IfCardHolderExist(holderName))
            {
                _paymentManager.InsertIfHolderNotExist(holderName, creditCardNumber, cvv, expirty);
                if (_paymentManager.GetLineCard(holderName) > amountToPay)
                {
                    return true;
                }

            }
            if (_paymentManager.GetLineCard(holderName) > amountToPay)
            {
                Repository.UpdateLine(amountToPay, holderName, creditCardNumber,cvv,expirty);
                return true;
            }
            else
                return false;
            }
            [WebMethod]
            public void SendPurchaseClearanceOnEmail(string ToEmail ,string creditCardNumber , List<CartModel> products)
            {
            DataTable data_table = _paymentManager.DetailtsOfAllInvites(creditCardNumber , products);
            string textBody = " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 400 + "><tr bgcolor='#4da6ff'><td><b> שם המוצר </b></td> <td> <b> מחיר </b> </td></tr>";
            for (int loopCount = 0; loopCount < data_table.Rows.Count; loopCount++)
            {
                textBody += "<tr><td>" + data_table.Rows[loopCount]["שם המוצר"] + "</td><td> " + data_table.Rows[loopCount]["מחיר המוצר"] + "</td> </tr>";
            }
            textBody += "</table>";
            MailMessage mail = new MailMessage();
            System.Net.Mail.SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("AmiamStore@gmail.com");
            mail.To.Add(ToEmail);
            mail.Subject = "Thank you for buying on Amiam Marketing website";
            mail.Body = textBody;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("AmiamStore@gmail.com", "vm0547788384");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        public class PaymentManager
        {

            CreditCardRepository DAL = new CreditCardRepository();

            public bool IsMasterCardHolder(string creditCardNumber)
            {
                if (CheckIfCreditCardNumberVerify(creditCardNumber))
                    if (creditCardNumber.Length == 16)
                        return true;
                return false;

            }
            public bool CheckIfCreditCardNumberVerify(string creditCardNumber)
            {
                if (creditCardNumber.Length == 16 || creditCardNumber.Length == 15)
                    return true;
                return false;
            }
            public bool IfCardHolderExist(string CardHolderName)
            {
                DataTable dt = DAL.GetCreditCards();
                foreach (DataRow dr in dt.Rows)
                {
                    if (CardHolderName == dr["CardHolder"].ToString())
                        return true;
                }
                return false;
            }
            public void InsertIfHolderNotExist(string holderName, string creditCardNumber, string cvv, string expirityDate)
            {
                CreditCard card = new CreditCard();
                card.CardHolder = holderName;
                card.CreditCardNumber = creditCardNumber;
                card.CVV = cvv;
                card.ExpiryDate = expirityDate;
                card.LineOfCredit = 1000;//כל לקוח חדש במערכת מקבל גבול של עד מאה שקלים
                DAL.InsertCredit(card);
            }
            public int GetLineCard(string CardHolder)
            {
                DataTable dt = DAL.GetCreditCards();
                foreach (DataRow dr in dt.Rows)
                {
                    if (CardHolder == dr["CardHolder"].ToString())
                        return (int)dr["LineOfCredit"];
                }
                return 0;
            }
            public DataTable ConvertListToDatatable(List<CartModel> products)
            {
                PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(CartModel));
                DataTable table = new DataTable();
                int index = 1;
                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name);
                foreach (CartModel item in products)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        if (index % 2 != 0)
                            row[prop.Name] = item.product.ProductName;
                        if (index % 2 == 0)
                            row[prop.Name] = item.product.ProductPrice;
                        index++;
                    }
                    table.Rows.Add(row);
                }
                return table;
            }
            public DataTable DetailtsOfAllInvites(string creditCardNumber, List<CartModel> products)
            {
                DataTable dt = ConvertListToDatatable(products);
                int index = 0;
                DataTable NewDt = new DataTable();
                NewDt.Columns.Add("שם המוצר");
                NewDt.Columns.Add("מחיר המוצר");
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow toInsert = NewDt.NewRow();
                    toInsert["שם המוצר"] = dr["product"].ToString();
                    toInsert["מחיר המוצר"] = dr["Quantity"].ToString();
                    NewDt.Rows.InsertAt(toInsert, index);
                    index++;
                }
                return NewDt;
            }

        }
        public class PaymentMethod
        {

            public string CardHolder { get; set; }
            public string CreditCardNumber { get; set; }
            public string Cvv { get; set; }
            public string ExpiryDate { get; set; }


            public PaymentMethod() { }
            public PaymentMethod(string cardHolder, string creditCardNumber, string cvv, string expiryDate)
            {
                CardHolder = cardHolder;
                CreditCardNumber = creditCardNumber;
                Cvv = cvv;
                ExpiryDate = expiryDate;
            }
        }
    }
}
