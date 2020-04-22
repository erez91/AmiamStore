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
            var manager = new AuthenticationManager();
            return manager.GetUser();
        }

    }
}