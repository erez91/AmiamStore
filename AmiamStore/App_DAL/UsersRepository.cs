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
            var query = string.Format(@"INSERT INTO Users (UserName, [Password], UserType , UserID) VALUES ('{0}','{1}',{2} ,'{3}');",
                user.Username, user.Password, user.UserType , user.UserID
                );
            _dbHeloer.ExecuteNonQuery(query);
        }
        public bool IfUserNameExcist(User user)
        {
            bool b = false;
            DataTable dt = GetUserNameOfUsers();
            foreach(DataRow dr in dt.Rows)
            {
                if (dr["UserName"].ToString() == user.Username)
                    b = true;//השם משתמש קיים במסד נתונים
            }
            return b;
        }
    }
}