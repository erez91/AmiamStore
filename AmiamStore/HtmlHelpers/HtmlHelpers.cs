using AmiamStore.App_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmiamStore.HtmlHelpers
{
    public static class HtmlHelpers
    {
        public static CurrentUser GetCurrentUser(this HtmlHelper htmlHelper)
        {
            var manager = new AthenticationManager();
            return manager.GetUser();
        }
        public static int GetCustomerID(this HtmlHelper htmlHelper)
        {
            var manager = new AthenticationManager();
            return (int)manager.GetUser().UserID;
        }
    }
}