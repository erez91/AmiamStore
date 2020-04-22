using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AmiamStore.Models;
using AmiamStore.App_BLL;
using AmiamStore.App_DAL;

namespace AmiamStore.App_BLL
{
    public class ProductBLL
    {
        public ProductModel getProduct(int id)
        {
           ProductDAL dal = new ProductDAL();
           DataTable dt = dal.getProduct(id);

            // converting from a DataTable to a Product Object!
            ProductModel prod = new ProductModel();
            foreach (DataRow dr in dt.Rows)
            {
                prod.ProductID = id;
                prod.ProductName = dr["ProductName"].ToString();
                prod.ProductImage = dr["ProductImage"].ToString();
                prod.ProductPrice = (int)dr["ProductPrice"];
                prod.ProductDescription = dr["ProductDescription"].ToString();
            }
            return prod;
        }

        public ProductsPageModel getProductById(int id)
        {
            ProductDAL dal = new ProductDAL();
            DataTable dt = dal.getProduct(id);

            // converting from a DataTable to a Product Object!
            ProductsPageModel prod = new ProductsPageModel();
            List<ProductModel> products = new List<ProductModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductModel product = new ProductModel();
                product.ProductID = id;
                product.ProductName = dr["ProductName"].ToString();
                product.ProductImage = dr["ProductImage"].ToString();
                product.ProductPrice = (int)dr["ProductPrice"];
                product.ProductDescription = dr["ProductDescription"].ToString();
                products.Add(product);
            }
            prod.Products = products;
            return prod;
        }
    }
}