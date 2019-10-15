
using Microsoft.AspNetCore.Http;
using SoftCRP.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftCRP.Web.Models
{
    public class UserRoleViewModel : User
    {
        [Display(Name = "Foto")]
        public IFormFile FotoFile { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
