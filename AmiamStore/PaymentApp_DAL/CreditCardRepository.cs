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
            var query = string.Format(@"INSERT INTO CreditCards (CreditCardNumber, CVV, ExpiryDate, CardHolder , LineOfCredit) VALUES ('{0}', '{1}', '{2}', '{3}','{4}')",
                Credit.CreditCardNumber,
                Credit.CVV,
                Credit.ExpiryDate,
                Credit.CardHolder,
                Credit.LineOfCredit
                );
            _dbHelper.ExecuteNonQuery(query);
        }

        public bool IfCardExist(string CardHolder , string CreditCardNumber)
        {
            DataTable dt = GetCreditCards();
            foreach(DataRow dr in dt.Rows)
            {
                if (dr["CardHolder"].ToString() == CardHolder && dr["CreditCardNumber"].ToString() == CreditCardNumber)
                    return true;
            }
            return false;
        }

        public void UpdateNewLine(double AmountToReduce , string CardHolder , string CardNumber , string cvv, string expirty)
        {
            DataTable dt = GetCreditCards();
            foreach (DataRow dr in dt.Rows)
            {
                if(dr["CardHolder"].ToString() == CardHolder && dr["CreditCardNumber"].ToString() == CardNumber)
                {
                    double NewAmountLine = (((int)dr["LineOfCredit"]) - AmountToReduce);
                    var query = string.Format(@"UPDATE CreditCards
                                                SET [LineOfCredit] = '" + NewAmountLine +
                                                "'WHERE [CreditCardNumber] ='" + CardNumber + "'");
                    _dbHelper.ExecuteNonQuery(query);
                }
            }
            }
        public void UpdateLine(double AmountToReduce, string CardHolder, string CardNumber, string cvv, string expirty)
        {
            DataTable dt = GetCreditCards();
            var CurrnetLine = string.Format(@" SELECT CreditCards.LineOfCredit
                                               FROM CreditCards
                                               WHERE [CreditCardNumber] ='" + CardNumber + "'");
           DataTable Newdt =  _dbHelper.GetData(CurrnetLine);
            int current = GetSpacificLine(Newdt);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CardHolder"].ToString() == CardHolder && dr["CreditCardNumber"].ToString() == CardNumber)
                {
                    double NewLine = current - AmountToReduce;
                    var query = string.Format(@"UPDATE CreditCards
                                                SET [LineOfCredit] = '" + NewLine +
                                                "'WHERE [CreditCardNumber] ='" + CardNumber + "'");
                    _dbHelper.ExecuteNonQuery(query);
                }
            }
        }
        public int GetSpacificLine(DataTable dt)
        {
            int x = (int)dt.Rows[0][0];
            return x;
        }
    }
}