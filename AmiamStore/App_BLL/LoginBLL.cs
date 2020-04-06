using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AmiamStore.App_DAL;
using AmiamStore.Models;
using AmiamStore.App_BLL.Entities;

namespace AmiamStore.App_BLL
{
    public class LoginBLL
    {
        public List<User> GetUsers()
        {
            UsersRepository dal = new UsersRepository();
            DataTable dt = dal.GetUsers();

            // converting from a DataTable to a Product Object!
            List<User> listUsers = new List<User>();
            foreach (DataRow dr in dt.Rows)
            {
                User d = new User();
                d.Username = dr["UserName"].ToString();
                d.Password = dr["Password"].ToString();
                d.UserType = (int)dr["UserType"];
                d.UserID = (int)dr["UserID"];
                listUsers.Add(d);
            }
            return listUsers;
        }

        public int GetUserIdByUserName(string username)
        {
            UsersRepository dal = new UsersRepository();
            DataTable dt = dal.GetUsers();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["UserName"].ToString() == username)
                {
                    return (int)dr["UserID"];
                }
            }
            throw new Exception("Error - user was not found");

        }

        public User GetSingleUser(string userName, string password)
        {
            var users = GetUsers();
            try
            {
                return users.Single(u => u.Username == userName && u.Password == password);
            }
            catch
            {
                throw new Exception("Error on getting user. Error Number: 57853");
            }

        }

    }
}