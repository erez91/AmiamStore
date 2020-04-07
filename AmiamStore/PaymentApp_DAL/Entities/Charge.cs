using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmiamStore.PaymentApp_DAL.Entities
{
    public class Charge
    {
        public string CreditCardNumber { get; set; }
        public int AmountToCharge { get; set; }
        public string StoreName { get; set; }
    }
}