using AmiamStore.App_BLL;
using AmiamStore.App_DAL;
using AmiamStore.Controllers.BaseControllers;
using AmiamStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmiamStore.Controllers
{
    public class ProductsPageController : BaseController
    {
        public ProductsPageController() : base(false) { }
        private ProductsPageBLL _productsService = new ProductsPageBLL();

        private string strCart = "Cart";
        [HttpGet]
        public ActionResult ProductsSearchPage(string productName)
        {
            var model =_productsService.SearchProducts(productName);
            return View("ProductsPage", model);
        }

        public ActionResult ProductsPage(int id, int? productId)
        {
            if (productId.HasValue)
            {
                AddProductToCart(productId.Value);
            }
            var products = _productsService.GetProductsByCata(id);
            TempData["ListProduts"] = products.Products;
            ProductsPageModel model = GetModel(products.Products);
            return View(model);
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


        private ProductsPageModel GetModel(List<ProductModel> products)
        {
            ProductsPageModel model = new ProductsPageModel();
            string CDescrip = "";
            model.Products = new List<ProductModel>();
            foreach (var product in products)
            {
                CDescrip = product.CatagoryDescription;
                model.Products.Add(new ProductModel() { ProductID = product.ProductID, ProductName = product.ProductName, ProductImage = product.ProductImage, ProductPrice = product.ProductPrice , CatagoryDescription = product.CatagoryDescription, CategoryId = product.CategoryId});
            }
            model.CatagoryDescription = CDescrip;
            return model;
        }

        //Option 2
        //private ProductsPageModel GetModel(List<Product> products)
        //{
        //    ProductsPageModel model = new ProductsPageModel();
        //    model.Products = products.Select(p => new ProductModel() { ProductName = p.ProductName, ProductImage = p.ProductImage, ProductPrice = p.ProductPrice }).ToList();
        //    return model;

        //}
    }
}