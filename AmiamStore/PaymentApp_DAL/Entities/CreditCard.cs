using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmiamStore.PaymentApp_DAL.Entities
{
    public class CreditCard
    {
        public string CreditCardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryDate { get; set; }
        public string CardHolder { get; set; }
        public int LineOfCredit { get; set; }

    }
}