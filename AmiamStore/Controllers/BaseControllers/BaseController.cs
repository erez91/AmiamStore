using AmiamStore.App_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmiamStore.Controllers.BaseControllers
{
    public class BaseController : Controller
    {
        private readonly bool _requireAuthentication;
        private readonly UserType? _userType = null;

        private readonly AuthenticationManager _authenticationManager = new AuthenticationManager();
        protected AuthenticationManager AuthenticationManager
        {
            get
            {
                return _authenticationManager;
            }
        }
        protected BaseController(UserType type)
        {
            _userType = type;
        }

        protected BaseController(bool requireAuthentication)
        {
            _requireAuthentication = requireAuthentication;
        }
        protected BaseController() : this(true)
        {
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUser = _authenticationManager.GetUser();
            if (currentUser == null && _requireAuthentication)
            {
                Response.Redirect("/Login/LoginPage");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}