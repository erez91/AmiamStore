using AmiamStore.Controllers.BaseControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmiamStore.App_DAL;
using System.Data;
using AmiamStore.Models;
using System.Web.UI.WebControls;

namespace AmiamStore.Controllers
{
    public class ShareController : BaseController
    {
        public ShareController() : base(false) { }
        // GET: Share
        public ActionResult SearchBox()
        {

            return View();
        }
        public List<ProductModel> ConvertDataTableToList(string ProductSerch)
        {
            MasterDAL c = new MasterDAL();
            DataTable dt = c.GetProductByName(ProductSerch);
            List<ProductModel> productsSerch = new List<ProductModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductModel product = new ProductModel();
                product.ProductID = (int)dr["ProductID"];
                product.ProductName = dr["ProductName"].ToString();
                product.ProductImage = dr["ProductImage"].ToString();
                product.ProductPrice = (int)dr["ProductPrice"];
                product.ProductDescription = dr["ProductDescription"].ToString();
                productsSerch.Add(product);
            }
            return productsSerch;
        }

        //[HttpGet]
        //public ActionResult SerchTable(string ProductName)
        //{
        //    MasterDAL c = new MasterDAL();
        //    List<ProductModel> ProductSerchResult = ConvertDataTableToList(ProductName);
        //    ProductsPageModel model = new ProductsPageModel();
        //    model.Products = ProductSerchResult;
        //    model.SerchedProductName = ProductName;
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult SerchTable(ProductsPageModel model)
        //{
        //    MasterDAL c = new MasterDAL();
        //    List<ProductModel> ProductSerchResult = ConvertDataTableToList(model.SerchedProductName);
        //    model.Products = ProductSerchResult;
        //    model.SerchedProductName = model.SerchedProductName;
        //    return View(model);
        //}
    }
}