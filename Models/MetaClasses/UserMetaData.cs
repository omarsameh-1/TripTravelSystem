using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripTravelSystem.Models
{
    public class UserMetaData
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is Required")]
        [StringLength(50)]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is Required")]
        [StringLength(50)]
        public string lastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Address is Required")]
        //[EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "Image is Required.")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public string photo { get; set; }

        [Range(1, 3)]
        public int roleTypeID { get; set; }

        [Display(Name = "PassWord")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minmum Length is 8")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$)",ErrorMessage ="PassWord should contain at least one of those [a-z] , at least one of those [A-Z], at least one spetial character, Minmum lenght is 8 and Maxmum lenght is 15")]
        public string password { get; set; }

        [Display(Name = "Confirm PassWord")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "PassWord Not Matched")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$)", ErrorMessage = "PassWord should contain at least one of those [a-z] , at least one of those [A-Z], at least one spetial character, Minmum lenght is 8 and Maxmum lenght is 15")]
        public string ConfirmPassword { get; set; }


        public bool isEmailVerified { get; set; }


        public System.Guid ActivationCode { get; set; }
    }

    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }
}
