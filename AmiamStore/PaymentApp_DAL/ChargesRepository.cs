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
        private readonly DBHelper _dbHelper = new DBHelper("Payment.accdb");

        public DataTable GetCharges()
        {
            string sql =
              @" SELECT Charges.CreditCardNumber,Charges.StoreName , Charges.ProductName, Charges.ProductPrice
                 FROM Charges;";
            DataTable dt = _dbHelper.GetData(sql);
            return dt;
        }

        public void InsertCharge(Charge Charge)
        {
            var query = string.Format(@"INSERT INTO Customers (CreditCardNumber, AmountToCharge, StoreName , ProductName , ProductPrice) VALUES ('{0}', '{1}', '{2}' , '{3}' , '{4}')",
                Charge.CreditCardNumber,
                Charge.AmountToCharge,
                Charge.StoreName,
                Charge.ProductName,
                Charge.ProductPrice
                );
            _dbHelper.ExecuteNonQuery(query);
        }

    }
}