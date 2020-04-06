using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmiamStore.Models
{
    public class ProductsPageModel
    {
        public List<ProductModel> Products { get; set; }
        public string CatagoryDescription { get; set; }
        public List<ProductModel> FiveRandomProducts { get; set; }
        public int[] MostPopularProducts { get; set; }
        public string PotionalCustomerName { get; set; }
        public string PotionalCustomerCompany { get; set; }
        public string PotionalCustomerPhone { get; set; }
    }
}