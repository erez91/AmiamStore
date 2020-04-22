using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AmiamStore.App_DAL;
using AmiamStore.Models;
using AmiamStore.HtmlHelpers;

namespace AmiamStore.App_BLL
{
    public class ProductsPageBLL
    {
        private readonly DBHelper _dbHelper = new DBHelper();

        public ProductsPageModel GetProductsByCata(int catagoryID)
        {
            ProductsPageDAL dal = new ProductsPageDAL();
            DataTable dt = dal.getProductByCatagorie(1);

            // converting from a DataTable to a Product Object!
            ProductsPageModel d = new ProductsPageModel();
            List<ProductModel> products = new List<ProductModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductModel product = new ProductModel();
                product.ProductID = (int)dr["ProductID"];
                product.ProductName = dr["ProductName"].ToString();
                product.ProductImage = dr["ProductImage"].ToString();
                product.ProductPrice = (int)dr["ProductPrice"];
                product.ShipperID = dr["ShipperID"].ToString();
                product.ProductDescription = dr["ProductDescription"].ToString();
                products.Add(product);
            }
            d.Products = products;
            return d;
        }

        private List<ProductModel> GetProducts(DataTable dataTable)
        {
            List<ProductModel> products = new List<ProductModel>();
            foreach (DataRow dr in dataTable.Rows)
            {
                ProductModel product = new ProductModel();
                product.ProductID = (int)dr["ProductID"];
                product.ProductName = dr["ProductName"].ToString();
                product.ProductImage = dr["ProductImage"].ToString();
                product.ProductPrice = (int)dr["ProductPrice"];
                product.ProductDescription = dr["ProductDescription"].ToString();
                products.Add(product);
            }
            return products;

        }

        public ProductsPageModel SearchProducts(string productName)
        {
            ProductsPageDAL dal = new ProductsPageDAL();
            DataTable dt = dal.GetProductByName(productName);
            ProductsPageModel d = new ProductsPageModel();
            d.Products = GetProducts(dt);
            return d;
        }

        public ProductsPageModel GetProductsList()
        {
            ProductsPageDAL dal = new ProductsPageDAL();
            DataTable dt = dal.getProducts();

            // converting from a DataTable to a Product Object!
            ProductsPageModel d = new ProductsPageModel();
            List<ProductModel> products = new List<ProductModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductModel product = new ProductModel();
                product.ProductID = (int)dr["ProductID"];
                product.ProductName = dr["ProductName"].ToString();
                product.ProductImage = dr["ProductImage"].ToString();
                product.ProductPrice = (int)dr["ProductPrice"];
                product.ProductDescription = dr["ProductDescription"].ToString();
                products.Add(product);
            }
            d.Products = products;
            return d;
        }
        public int[] MostPopularProducts()
        {
            string sql =
           @" SELECT ProductID
                   FROM  Orders";
            DataTable dt = _dbHelper.GetData(sql);
            var AllNumbersFromDatatable = GetAllNumbersFromDatatable(dt).ToArray();
            int[] MostPopularProducts = new int[3];
            for(int i = 0;i < MostPopularProducts.Length;i++)
            {
                int mostCommonValue = MostCommonNumberInArray(AllNumbersFromDatatable)[0];
                MostPopularProducts[i] = mostCommonValue;
                AllNumbersFromDatatable = DeleteMostCommonNumber(AllNumbersFromDatatable, mostCommonValue);
            }
            return MostPopularProducts;
        }
        public IEnumerable<int> GetAllNumbersFromDatatable(DataTable dt)
        {
            foreach(DataRow row in dt.Rows)
            {
                for(int i = 0;i< dt.Columns.Count;i++)
                {
                    var value = row[i];
                    if(value is int)
                    {
                        yield return (int)value;
                    }
                }
            }
        }
        public int[] MostCommonNumberInArray(int[] arr)
        {
            var cnt = new Dictionary<int, int>();
            int[] ValueAndCount = new int[2];
            foreach (int value in arr)
            {
                if (cnt.ContainsKey(value))
                {
                    cnt[value]++;
                }
                else
                {
                    cnt.Add(value, 1);
                }
            }
            int mostCommonValue = 0;
            int highestCount = 0;
            foreach (KeyValuePair<int, int> pair in cnt)
            {
                if (pair.Value > highestCount)
                {
                    mostCommonValue = pair.Key;
                    highestCount = pair.Value;
                }
            }
            ValueAndCount[0] = mostCommonValue;
            ValueAndCount[1] = highestCount;
            return ValueAndCount;
        }
        public int[] DeleteMostCommonNumber(int[] arr , int number)
        {
            arr = arr.Where(val => val != number).ToArray();
            return arr;
        }
    }
}
