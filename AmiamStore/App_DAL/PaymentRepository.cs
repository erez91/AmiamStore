using AmiamStore.App_BLL.Entities;
using AmiamStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmiamStore.App_DAL
{
    public class PaymentRepository
    {
        private readonly DBHelper _dbHelper = new DBHelper();
       
        public DataTable GetPayment()
        {
            string sql =
             @"SELECT Payment.ExpiryDate, Payment.HolderName, Payment.CVV, Payment.CreditCardNumber, Payment.OrderID, Payment.CustomerID
               FROM   Payment;";
            DataTable dt = _dbHelper.GetData(sql);
            return dt;
        }
        public void Insert(CartViewModel paymentMethood)
        {
            var query = string.Format(@"INSERT INTO Payment (CustomerID , OrderID , CreditCardNumber, CVV , HolderName, ExpiryDate) VALUES ('{0}','{1}','{2}', '{3}' , '{4}' , '{5}');",
             paymentMethood.CustomerID , paymentMethood.OrderID , paymentMethood.CreditCardNumber, paymentMethood.Cvv, paymentMethood.CardHolder, paymentMethood.ExpiryDate
                );
            _dbHelper.ExecuteNonQuery(query);
        }
    }
}