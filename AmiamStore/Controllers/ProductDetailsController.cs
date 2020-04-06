using AmiamStore.Models;
using System.Web.Mvc;
using AmiamStore.App_BLL;
using AmiamStore.Controllers.BaseControllers;
using System.Linq;
using AmiamStore.App_DAL;
using System.Collections.Generic;

namespace AmiamStore.Controllers
{
    public class ProductDetailsController : BaseController
    {
        public ProductDetailsController() : base(UserType.Client) { }
        // GET: ProductDetails
        private ProductsPageBLL _productsService = new ProductsPageBLL();
        private string strCart = "Cart";

        public ActionResult ProductDetails(int? id)
        {
            ProductBLL bll = new ProductBLL();
            ProductsPageDAL DAL = new ProductsPageDAL();
            ProductsPageModel prod = bll.getProductById((int)id);
            prod.FiveRandomProducts = DAL.FiveRandomProducts((int)id);
            if (id.Value == prod.FiveRandomProducts[0].ProductID || id.Value == prod.FiveRandomProducts[1].ProductID || id.Value == prod.FiveRandomProducts[2].ProductID || id.Value == prod.FiveRandomProducts[3].ProductID)
            {
                AddProductToCart((int)id.Value);
            }
            return View(prod);
        }
        private void AddProductToCart(int productId)
        {
            ProductBLL bll = new ProductBLL();

            if (Session[strCart] == null)
            {
                List<CartModel> listCart = new List<CartModel>
                {
                    new CartModel(bll.getProduct((int)productId) , 1)
                };
                Session[strCart] = listCart;
            }
            else
            {
                List<CartModel> listCart = (List<CartModel>)Session[strCart];
                int check = IsExistCheck(productId);
                if (check == 0)
                    listCart.Add(new CartModel(bll.getProduct(productId), 1));
                else
                    listCart[check].Quantity++;

                Session[strCart] = listCart;
            }
        }

        private int IsExistCheck(int id)
        {
            List<CartModel> listCart = (List<CartModel>)Session[strCart];
            for (int i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].product.ProductID == id)
                    return i;
            }
            return 0;
        }
    }
}