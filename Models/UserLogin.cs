using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripTravelSystem.Models
{
    public class UserLogin
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Address is Required!")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "PassWord is Required!")]
        [Display(Name = "PassWord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}