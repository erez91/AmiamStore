using AmiamStore.Models;
using System.Web.Mvc;
using AmiamStore.App_BLL;
using AmiamStore.Controllers.BaseControllers;
using System.Linq;

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
            prod.Products = bll.CommonProductsId(id);
            ViewBag.Message = prod;
            return View(prod);
        }
    }
}