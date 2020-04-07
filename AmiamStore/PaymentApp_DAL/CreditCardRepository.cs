using AmiamStore.App_DAL;
using AmiamStore.PaymentApp_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmiamStore.PaymentApp_DAL
{
    public class CreditCardRepository
    {
        private readonly DBHelper _dbHelper = new DBHelper("Payment.accdb");

        public DataTable GetCreditCards()
        {
            string sql =
              @" SELECT CreditCards.CreditCardNumber, CreditCards.CVV, CreditCards.ExpiryDate, CreditCards.CardHolder, CreditCards.LineOfCredit
                 FROM CreditCards;";
            DataTable dt = _dbHelper.GetData(sql);
            return dt;
        }

        public void InsertCredit(CreditCard Credit)
        {
            var query = string.Format(@"INSERT INTO Customers (CreditCardNumber, CVV, ExpiryDate, CardHolder , LineOfCredit) VALUES ('{0}', '{1}', '{2}', '{3}','{4}')",
                Credit.CreditCardNumber,
                Credit.CVV,
                Credit.ExpiryDate,
                Credit.CardHolder,
                Credit.LineOfCredit
                );
            _dbHelper.ExecuteNonQuery(query);
        }

    
    }
}