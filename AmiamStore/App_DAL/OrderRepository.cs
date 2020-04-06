using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmiamStore.Models;
namespace AmiamStore.App_DAL
{
    public class OrderRepository
    {
        private readonly DBHelper _dbHelper = new DBHelper();

        public void Insert(CartViewModel Order)
        {
                DateTime dateTime = DateTime.UtcNow.Date;
                foreach(var product in Order.Products)
            {
                var query = string.Format(@"INSERT INTO Orders (OrderDate, ProductPrice, ProductID, ProductQuantity) VALUES ('{0}','{1}', '{2}', '{3}');",
                      dateTime, product.product.ProductPrice, product.product.ProductID , product.Quantity);
                _dbHelper.ExecuteNonQuery(query);
            }
        }
        public int GetLastOrderID()
        {
            var query = string.Format(@"SELECT Max(OrderID) AS MaxOrder
                                        FROM   Orders;");
           DataTable dt =  _dbHelper.GetData(query);
            return (int)dt.Rows[0].ItemArray[0];
        }
    }
}