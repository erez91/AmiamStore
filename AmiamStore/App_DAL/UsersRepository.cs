using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AmiamStore.App_BLL.Entities;

namespace AmiamStore.App_DAL
{
    public class UsersRepository
    {
       private readonly DBHelper _dbHeloer = new DBHelper();

        public DataTable GetUsers()
        {
            string sql =
               @" SELECT Users.UserName, Users.Password, Users.UserID, Users.UserType
                   FROM  Users;";
            DataTable dt = _dbHeloer.GetData(sql);
            return dt;
        }
        public DataTable GetUserNameOfUsers()
        {
            string sql =
               @" SELECT Users.UserName
                   FROM  Users;";
            DataTable dt = _dbHeloer.GetData(sql);
            return dt;
        }
        public void Insert(User user)
        {
            var query = string.Format(@"INSERT INTO Users (UserName, [Password], UserType) VALUES ('{0}','{1}',{2});",
                user.Email, user.Password, user.UserType
                );
            _dbHeloer.ExecuteNonQuery(query);
        }
        public bool IfUserNameExcist(User user)
        {
            bool b = false;
            DataTable dt = GetUserNameOfUsers();
            foreach(DataRow dr in dt.Rows)
            {
                if (dr["UserName"].ToString() == user.Email)
                    b = true;//השם משתמש קיים במסד נתונים
            }
            return b;
        }
        public User GetUserByUserName(string token)
        {
            bool b = false;
            User user = new User();
            DataTable dt = GetUsers();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["UserName"].ToString() == token)
                {
                    user.Email = token;
                    user.Password = dr["Password"].ToString();
                    user.UserType = (int)dr["UserType"];
                }
            }
            return user;
        }
    }
}