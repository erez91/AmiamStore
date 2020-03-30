using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using AmiamStore.App_DAL;
namespace AmiamStore
{
    /// <summary>
    /// Summary description for PaymentWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PaymentWebService : System.Web.Services.WebService
    {
        public static PaymentManager _paymentManager = new PaymentManager();

        [WebMethod]//push
        public double DiscountForMasterCardClients(string holderName, string creditCardNumber, string cvv, string expirityDate, double amountToPay)
        {
            if (_paymentManager.IsMasterCardHolder(creditCardNumber) && _paymentManager.Pay(holderName, creditCardNumber, cvv, expirityDate, amountToPay))
            {
                double UpdatedAmountToPay = ((amountToPay * 100) / 10);
                return UpdatedAmountToPay;
            }
            else
                return -1;
        }
    }

    public class PaymentManager
    {
        private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();

        public bool Pay(string holderName, string creditCardNumber, string cvv, string expirityDate, double amountToCharge)
        {
            PaymentMethod o = new PaymentMethod(holderName, creditCardNumber, cvv, expirityDate);
            List<PaymentMethod> payments = new List<PaymentMethod>();
            string month = expirityDate.Substring(0, 2);
            string year = expirityDate.Substring(4, 3);
            if (cvv.Length == 3 && creditCardNumber.Length == 19 && int.Parse(month) > 0 && int.Parse(month) <= 12 && int.Parse(year) > 20)
                return true;
            return false;
        }
        public bool IsMasterCardHolder(string creditCardNumber)
        {
         if(CheckIfCreditCardNumberVerify(creditCardNumber))
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
        public List<PaymentMethod> LoadSavedPayments()
        {
            List<PaymentMethod> SavedPayments = new List<PaymentMethod>(); PaymentRepository c = new PaymentRepository();
            DataTable dt  = c.GetPayment();
            foreach(DataRow dr in dt.Rows)
            {
                PaymentMethod m = new PaymentMethod();
                m.CardHolder = dr["HolderName"].ToString();
                m.CreditCardNumber = dr["CreditCardNumber"].ToString();
                m.Cvv = dr["Cvv"].ToString();
                m.ExpiryDate = dr["ExpiryDate"].ToString();
                _paymentMethods.Add(m);
            }
            return SavedPayments;
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
