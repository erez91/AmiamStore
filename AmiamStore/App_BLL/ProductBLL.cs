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
                prod.CatagoryName = dr["CatagoryName"].ToString();
                prod.CategoryId = (int)dr["CatagoryId"];
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
                product.CatagoryName = dr["CatagoryName"].ToString();
                product.CategoryId = (int)dr["CatagoryId"];
                products.Add(product);
            }
            prod.Products = products;
            return prod;
        }
        private int[] RandomArr(int CurrentId,int ListLength)
        {
            Random rng = new Random();
            int smallestNumber = 1;
            int biggestNumber = ListLength;

            //Determine the amount of random numbers
            int amountOfRandomNumbers = 4;

            //Create a list of numbers from which the routine
            //shall choose the result numbers
            var possibleNumbers = new List<int>();
            for (int i = smallestNumber; i <= biggestNumber; i++)
                possibleNumbers.Add(i);

            var result = new List<int>();

            //Initialize a random number generator
            Random rand = new Random();

            //For-loop which picks each round a unique random number
            for (int i = 0; i < amountOfRandomNumbers; i++)
            {
                //Generate random number
                int randomNumber = rand.Next(1, possibleNumbers.Count) - 1;
                if(randomNumber != CurrentId)
                    result.Add(possibleNumbers[randomNumber]);
                else
                {
                    randomNumber = rand.Next(1, possibleNumbers.Count) - 1;
                    result.Add(possibleNumbers[randomNumber]);
                }
                //Remove the chosen result number from possible numbers list
                possibleNumbers.RemoveAt(randomNumber);
            }
            return result.ToArray();
        }
        public List<ProductModel> CommonProductsId(int currentId, List<ProductModel> list)
        {
            int[] arr = NumbersOptions(list, currentId);
            List<ProductModel> model = new List<ProductModel>();
            for (int i = 0; i <= 3; i++)
            {
                model.Add(getProduct(arr[i]));
            }
            return model;

        }
        public int[] NumbersOptions(List<ProductModel> list, int CurrentId)
        {
            int[] arr = new int[list.Count() - 1];
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (list[i].ProductID != CurrentId)
                {
                    arr[i] = list[i].ProductID;
                }
                else
                    index = i;
            }
            arr[index] = list.Last().ProductID;
            Random random = new Random();
            int[] shuffled = new int[3];
            shuffled = arr.OrderBy(x => Guid.NewGuid()).Take(4).ToArray();
            return shuffled;
        }
    }
}