using AmiamStore.Models;
using System.Web.Mvc;
using AmiamStore.App_BLL;
using AmiamStore.Controllers.BaseControllers;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace AmiamStore.Controllers
{
    public class ProductDetailsController : BaseController
    {
        public ProductDetailsController() : base(UserType.Client) { }
        // GET: ProductDetails
        public ActionResult ProductDetails(int id)
        {
            ProductBLL bll = new ProductBLL();
            ProductsPageModel prod = bll.getProductById(id);
            object myObject = TempData["ListProduts"];
            TempData.Keep();
            List<ProductModel> list = ((IEnumerable)myObject).Cast<ProductModel>().ToList();
            prod.CommonProducts = bll.CommonProductsId(id , list);
            ViewBag.Message = prod;
            return View(prod);
        }
    }
}