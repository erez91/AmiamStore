using AmiamStore.App_DAL;
using AmiamStore.PaymentApp_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmiamStore.PaymentApp_DAL
{
    public class ChargesRepository
    {
        private readonly DBHelper _dbHelper = new DBHelper("Payment");

        public DataTable GetCharges()
        {
            string sql =
              @" SELECT Charges.CreditCardNumber, Charges.AmountToCharge, Charges.StoreName
                 FROM Charges;";
            DataTable dt = _dbHelper.GetData(sql);
            return dt;
        }

        public void InsertCharge(Charge Charge)
        {
            var query = string.Format(@"INSERT INTO Customers (CreditCardNumber, AmountToCharge, StoreName) VALUES ('{0}', '{1}', '{2}')",
                Charge.CreditCardNumber,
                Charge.AmountToCharge,
                Charge.StoreName
                );
            _dbHelper.ExecuteNonQuery(query);
        }

    }
}