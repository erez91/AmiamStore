using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmiamStore.Models;
using AmiamStore.App_BLL;
using AmiamStore.App_DAL;
using AmiamStore.Controllers.BaseControllers;

namespace AmiamStore.Controllers
{
    public class LoginController : BaseController
    {
        private readonly LoginBLL _loginService = new LoginBLL();

        public LoginController() : base(false) { }

        [HttpGet]
        public ActionResult LoginPage()
        {
            return View();

        }

        // GET: LoginPage
        [HttpPost]
        public ActionResult LoginPage(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var user = _loginService.GetSingleUser(model.UserName, model.Password);
                AuthenticationManager.SaveUser(user.Username, model.Password, user.UserType, user.UserID);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                model.Message = "Username or password are not correct.";
                return View(model);
            }



        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}