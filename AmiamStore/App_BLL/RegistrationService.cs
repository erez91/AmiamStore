using AmiamStore.App_BLL.Entities;
using AmiamStore.App_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmiamStore.App_BLL
{
    public class RegistrationService
    {
        private readonly UsersRepository _usersRepository = new UsersRepository();
        private readonly CustomersRepository _customersRepository = new CustomersRepository();
        private readonly LoginBLL _usersService = new LoginBLL();

        public void Register(User user, Customer customer)
        {
            if(!_usersRepository.IfUserNameExcist(user))
            {
                _usersRepository.Insert(user);
                customer.UserID = _usersService.GetUserIdByUserName(user.Email);
                _customersRepository.Insert(customer);
            }
            else
            {
                throw new InvalidOperationException("The UserName is Taken.");
            }
        }
    }
}