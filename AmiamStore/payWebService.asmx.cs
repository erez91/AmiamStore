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


            [WebMethod]//push
            public bool ConfirmPay(string holderName, string creditCardNumber, string cvv, string expirityDate, int amountToCharge, List<Charge> list)
            {
                if (ProperCardDetails(holderName, creditCardNumber, cvv, expirityDate, amountToCharge) && ClientHasEnoghMoney(holderName, amountToCharge))
                {
                    amountToCharge = DiscountForMasterCardClients(creditCardNumber, amountToCharge);
                    SendPurchaseClearanceOnEmail("liamivgi009@gmail.com", "liam ivgi");
                    list = DetailtsOfAllInvites(creditCardNumber);
                    return true;
                }
                return false;
            }



            [WebMethod]//push
            public bool ProperCardDetails(string holderName, string creditCardNumber, string cvv, string expirityDate, int amountToCharge)
            {
                PaymentMethod o = new PaymentMethod(holderName, creditCardNumber, cvv, expirityDate);
                List<PaymentMethod> payments = new List<PaymentMethod>();
                string month = expirityDate.Substring(0, 2);
                string year = expirityDate.Substring(4, 3);
                if (cvv.Length == 3 && creditCardNumber.Length == 19 && int.Parse(month) > 0 && int.Parse(month) <= 12 && int.Parse(year) > 20)
                    return true;
                return false;
            }
            [WebMethod]//push
            public int DiscountForMasterCardClients(string creditCardNumber, int amountToPay)
            {
                if (_paymentManager.IsMasterCardHolder(creditCardNumber))
                {
                    int UpdatedAmountToPay = ((amountToPay * 100) / 10);
                    return UpdatedAmountToPay;
                }
                else
                    return amountToPay;
            }
            [WebMethod]//push
            public bool ClientHasEnoghMoney(string holderName, int amountToPay)
            {
                if (_paymentManager.IfCardHolderExist(holderName) && _paymentManager.GetLineCard(holderName) > amountToPay)
                    return true;
                return false;
            }

            [WebMethod]//push
            public List<Charge> DetailtsOfAllInvites(string creditCardNumber)
            {
                ChargesRepository DAL = new ChargesRepository();
                List<Charge> list = new List<Charge>();
                DataTable dt = DAL.GetCharges();
                foreach (DataRow dr in dt.Rows)
                {
                    if (creditCardNumber == dr["creditCardNumber"].ToString())
                    {
                        Charge ch = new Charge();
                        ch.AmountToCharge = (int)dr["AmountToCharge"];
                        ch.CreditCardNumber = dr["creditCardNumber"].ToString();
                        ch.StoreName = dr["StoreName"].ToString();
                        list.Add(ch);
                    }
                }
                return list;
            }
            [WebMethod]//push
            public void SendPurchaseClearanceOnEmail(string ToEmail, string ToName)
            {
                var fromAddress = new MailAddress("AmiamStore@gmail.com", "Amiam Store");
                var toAddress = new MailAddress(ToEmail, ToName);
                const string fromPassword = "vm0547788384";
                const string subject = "Subject";
                const string body = "Body";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
        

        public class PaymentManager
        {
            private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();

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
                CreditCardRepository DAL = new CreditCardRepository();
                DataTable dt = DAL.GetCreditCards();
                foreach (DataRow dr in dt.Rows)
                {
                    if (CardHolderName == dr["CardHolder"].ToString())
                        return true;
                }
                return false;
            }
            public int GetLineCard(string CardHolder)
            {
                CreditCardRepository DAL = new CreditCardRepository();
                DataTable dt = DAL.GetCreditCards();
                foreach (DataRow dr in dt.Rows)
                {
                    if (CardHolder == dr["CardHolder"].ToString())
                        return (int)dr["LineOfCredit"];
                }
                return 0;
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
