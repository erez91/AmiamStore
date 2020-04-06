using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace AmiamStore.App_BLL
{
    public class AthenticationManager
    {
        public void SaveUser(string Email,string password, UserType userType , int? UserID)
        {
            var currentUser = new CurrentUser(Email,userType,UserID, password);
            SetSession("currentUser",currentUser);
        }
        public void SaveUser(string Email, string password , int userType, int? UserID)
        {
            SaveUser(Email,password,(UserType)userType, UserID);
        }

        public CurrentUser GetUser()
        {
            return GetSession("currentUser") as CurrentUser;
        }

        public void LogOut()
        {
            HttpContext.Current.Session.Abandon();
        }

        private void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        private object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }


    }

    public class CurrentUser
    {
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string Password { get; set; }
        public int? UserID { get; set; }

        public CurrentUser(string Email, UserType userType, int? UserID, string pass)
        {
            this.Email = Email;
            this.UserID = UserID;
            this.UserType = userType;
            this.Password = pass;
        }
    }

    public enum UserType
    {
        Manager =1,
        Client = 2,
        Guest = 3
    }
}