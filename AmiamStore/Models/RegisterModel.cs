using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AmiamStore.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="{0} is mandatory.")]
        [Display(Name = "שם מלא")]
        //[MinLength(3, ErrorMessage = "Username is mandatory.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company name is mandatory.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "{0} is mandatory.")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is mandatory.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "{0} is mandatory.")]
        public string Password { get; set; }

        public int CustomerID { get; set; }
    }
}