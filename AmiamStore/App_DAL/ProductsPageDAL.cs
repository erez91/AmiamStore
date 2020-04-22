using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace AmiamStore.App_DAL
{
    public class ProductsPageDAL
    {
        private DBHelper _dbh = new DBHelper();

        public DataTable getProductByCatagorie(int catagoryID)
        {
            String sql =
               @"  SELECT        p.ProductName, p.ProductImage, p.ProductPrice, p.ProductDescription, p.ShipperID, p.ProductID ,h.CatagoryDescription, h.CatagoryName
                     FROM            ((ProductCatagories pc INNER JOIN
                     Product p ON p.ProductID = pc.ProductID)
                     INNER JOIN  Catagories h
                      on pc.CatagoryID = h.CatagoryID)
                     WHERE        pc.CatagoryID =" + catagoryID;
            DataTable dt = _dbh.GetData(sql);
            return dt;
        }
        public DataTable GetProductByName(string productName)
        {
            var query = string.Format(
             @" SELECT      Product.ProductName, Product.ProductID , Product.ProductImage , Product.ProductPrice , Product.ProductDescription
                FROM        Product
                WHERE       Product.ProductName like '%{0}%'", productName);
            DataTable dt = _dbh.GetData(query);
            return dt;
        }

        public DataTable getProducts()
        {
            String sql =
               @"SELECT     Product.ProductImage, Product.ProductName, Product.ProductPrice, Product.ProductDescription, Product.ProductID
                 FROM       Product";  
            DataTable dt = _dbh.GetData(sql);
            return dt;
        }
    }
}