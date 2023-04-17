using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WIP.Models
{
    public class UserRegistrationModel
    {
        public int UserID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is requierd")]
        [Display(Name = "First Name")]
        public string FirstName  { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is requierd")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Organization Name is requierd")]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "This is not an email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is requierd")]
        public string Email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is requierd")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Need min 6 character")]
        public string Password { get; set; }


        [NotMapped] // Does not effect with your database
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }

        public bool EmailVerification { get; set; }

    }
}