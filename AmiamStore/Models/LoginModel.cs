using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmiamStore.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Username is mandatory.")]
        //[MinLength(3, ErrorMessage = "Username is mandatory.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        public string Password { get; set; }

        public string Message { get; set; }

    }
}