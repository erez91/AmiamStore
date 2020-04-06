using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AmiamStore.Models;
using AmiamStore.App_BLL;
using System.Reflection;

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
               @"SELECT     Product.ProductImage, Product.ProductName, Product.ProductPrice, Product.ProductDescription, Product.ProductID, Catagories.CatagoryName
                 FROM       Catagories, Product";  
            DataTable dt = _dbh.GetData(sql);
            return dt;
        }

        public List<ProductModel> FiveRandomProducts(int catID)
        {
            ProductsPageBLL BLL = new ProductsPageBLL();
            Random rnd = new Random();
            int ProductsLength = GetHighestNumber();
            List<ProductModel> products = new List<ProductModel>();
            for(int i = 0;i < 4;i++)
            {
                int rndNumber = rnd.Next(1, ProductsLength);
                var query = string.Format(
          @" SELECT     Product.ProductID, Product.ProductDescription, Product.ProductName, Product.ProductImage, Product.ProductPrice, Catagories.CatagoryName, ProductCatagories.CatagoryID
            FROM       Product INNER JOIN (Catagories INNER JOIN ProductCatagories ON Catagories.CatagoryID = ProductCatagories.CatagoryID) ON Product.ProductID = ProductCatagories.ProductID
            WHERE     (((Product.[ProductID])=" + rndNumber + "))");
                DataTable dt = _dbh.GetData(query);
                List<ProductModel> product = new List<ProductModel>();
                product = BindList(dt);
                products.Add(product.First());
            }
            return products;
        }

        public static List<ProductModel> BindList(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new ProductModel()
                                 {
                                     ProductID = Convert.ToInt32(rw["ProductID"]),
                                     ProductName = rw["ProductName"].ToString(),
                                     ProductImage = rw["ProductImage"].ToString(),
                                     ProductDescription = rw["ProductDescription"].ToString(),
                                     ProductPrice = Convert.ToInt32(rw["ProductPrice"]),
                                     CategoryId = Convert.ToInt32(rw["CatagoryId"])
                                 }).ToList();

            return convertedList;
        }
        public int GetHighestNumber()
        {
            var query = string.Format(
            @" SELECT      Product.ProductID 
                FROM        Product");
            DataTable dt = _dbh.GetData(query);
            int Heighst = 0;
            foreach(DataRow dr in dt.Rows)
            {
                int num = (int)dr["ProductID"];
                if (num > Heighst)
                    Heighst = num;
            }
            return Heighst;
        }
    }
}