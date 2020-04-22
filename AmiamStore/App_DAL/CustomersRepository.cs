using AmiamStore.App_BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AmiamStore.Models;

namespace AmiamStore.App_DAL
{
    public class CustomersRepository
    {
        private readonly DBHelper _dbHeloer = new DBHelper();

        public DataTable GetCustomers()
        {
            string sql =
              @" SELECT        CustomerName, CustomerCompanyName, CustomerID, CustomerEmailAdress, CustomerPhone
                  FROM            Customers;";
            DataTable dt = _dbHeloer.GetData(sql);
            return dt;
        }
        public void Insert(Customer customer)
        {
            var query = string.Format(@"INSERT INTO Customers (CustomerName, CustomerCompanyName, CustomerEmailAdress, CustomerPhone, UserID) VALUES ('{0}', '{1}', '{2}', '{3}','{4}')",
                customer.CustomerName,
                customer.CustomerCompanyName,
                customer.CustomerEmailAdress,
                customer.CustomerPhone,
                customer.UserID
                );
            _dbHeloer.ExecuteNonQuery(query);
        }

        public void InsertPotinoalCustomers(ProductsPageModel c)
        {
            var query = string.Format(@"INSERT INTO PotentialCustomers (FullName, CompanyName, PhoneNumber) VALUES ('{0}', '{1}', '{2}')",
               c.PotionalCustomerName, c.PotionalCustomerCompany, c.PotionalCustomerPhone
                );
            _dbHeloer.ExecuteNonQuery(query);
        }
        public void InsertPotinoalCustomers(string Phone)
        {
            var query = string.Format(@"INSERT INTO PotentialCustomers (PhoneNumber) VALUES ('{0}')",
               Phone
                );
            _dbHeloer.ExecuteNonQuery(query);
        }
    }
}