using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Models
{
    public class RegisterNewUserViewModel
    {
        [Display(Name = "RUC/Cedula")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]

        public string Cedula { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]       
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
                                 
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
