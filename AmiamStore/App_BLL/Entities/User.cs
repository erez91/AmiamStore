using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmiamStore.App_BLL.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public int UserID { get; set; }

    }
}