using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AmiamStore.App_DAL
{
    public class ProductDAL
    {
        public DataTable getProduct(int id)
        {
            DBHelper dbh = new DBHelper();
            String sql =
          @"SELECT     Product.ProductID, Product.ProductDescription, Product.ProductName, Product.ProductImage, Product.ProductPrice
            FROM       Product
            WHERE     (((Product.[ProductID])=" + id + "));"; 
           
            DataTable dt = dbh.GetData(sql);
            return dt;
        }
    }
}