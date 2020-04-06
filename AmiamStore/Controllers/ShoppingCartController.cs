using AmiamStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmiamStore.App_DAL;
using System.Net.Mail;
using AmiamStore.App_BLL.Entities;
using System.Reflection;
using AmiamStore.App_BLL;

namespace AmiamStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        OrderRepository Order = new OrderRepository();
        AthenticationManager manger = new AthenticationManager();
        ProductBLL bll = new ProductBLL();
        private string strCart = "Cart";
        
        [HttpGet]
        //public ActionResult CartView()
        public ActionResult CartView()
        {
            CartViewModel model = new CartViewModel();
            if (manger.GetUser().UserID != null)
            {
                model.CustomerID = (int)manger.GetUser().UserID;
                model.OrderID = (Order.GetLastOrderID()) + 1;
            }
            model.Products = GetCart();
            model.OrderAmount = GetAmountToCharge();
            return View(model);
        }
        [HttpPost]
        public ActionResult CartView(CartViewModel c)
        {
            if (manger.GetUser().UserID != null)
            {
                c.CustomerID = (int)manger.GetUser().UserID;
                c.OrderID = (Order.GetLastOrderID()) + 1;
            }
            var paymentWebService = new PaymentServiceReference.PaymentWebServiceSoapClient();
            bool p = paymentWebService.Pay(c.CardHolder, c.CreditCardNumber, c.Cvv, c.ExpiryDate, GetAmountToCharge());
            if (p == true)
            {
                PaymentRepository Repository = new PaymentRepository();
                Repository.Insert(c);
                CartViewModel model = new CartViewModel();
                model.Products = GetCart();
                model.OrderAmount = GetAmountToCharge();
                Order.Insert(model);
                return RedirectToAction("OrderComplete");
            }
            return RedirectToAction("OrderFailed");
        }
        public ActionResult OrderComplete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OrderComplete(CartViewModel c)
        {
            return View(c);
        }
        public ActionResult OrderFailed()
        {
            return View();
        }
        private int GetAmountToCharge()
        {
            return GetCart().Sum(x => x.Quantity * x.product.ProductPrice).Value;
        }
        private List<CartModel> GetCart()
        {
            object cartList = Session[strCart];
            return cartList == null ? new List<CartModel>() : (List<CartModel>)cartList;
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            int check = IsExistCheck((int)id);
            List<CartModel> listCart = (List<CartModel>)Session[strCart];
            listCart.RemoveAt(check);
            CartViewModel model = new CartViewModel();
            model.Products = listCart;
            return View("CartView" , model);
        }

    }
}