using AmiamStore.Controllers.BaseControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmiamStore.Models;
using AmiamStore.App_BLL;
using AmiamStore.App_BLL.Entities;
using System.Net.Mail;
using System.Net;

namespace AmiamStore.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly RegistrationService _registrationService = new RegistrationService();

        public RegisterController() : base(false) { }
       
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                User user = new User()
                {
                    Email = model.Email,
                    Password = model.Password,
                    UserType = (int)UserType.Client
                };
                Customer customer = new Customer()
                {
                    CustomerCompanyName = model.CompanyName,
                    CustomerEmailAdress = model.Email,
                    CustomerName = model.Name,
                    CustomerPhone = model.Phone,
                    CustomerID = model.CustomerID
                };
                 
                _registrationService.Register(user, customer);
                return RedirectToAction("LoginPage", "Login");
            }
            catch (Exception e)
            {
                return RedirectToAction("UserNameError", "Register");
            }
        }

        public ActionResult UserNameError()
        {
            return View();
        }
    }
}