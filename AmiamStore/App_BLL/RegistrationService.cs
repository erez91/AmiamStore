using AmiamStore.App_BLL.Entities;
using AmiamStore.App_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
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

        public void SendEmailRegister(string email, string name)
        {
            string firstPart = "שלום";
            firstPart = firstPart + new string(' ', 10);
            firstPart = firstPart + name;
            firstPart = firstPart + new string(',', 1);
            firstPart = firstPart + new string(' ', 10);
            string secondPart = "תודה על הרשמתך לאתר עמיאם!אחת החברות המובילות בארץ בתחום ציוד הבטיחות. נשמח לראותך שוב באתר שלנו ותמיד לשרותך";
            string textBody = firstPart + secondPart;
            MailMessage mail = new MailMessage();
            System.Net.Mail.SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("AmiamStore@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Amiam Marketing";
            mail.Body = textBody;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("AmiamStore@gmail.com", "vm0547788384");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}