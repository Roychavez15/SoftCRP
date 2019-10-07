
using SoftCRP.Web.Data.Entities;
using System.Collections.Generic;

namespace SoftCRP.Web.Models
{
    public class UserRoleViewModel : User
    {
        public List<RoleViewModel> Roles { get; set; }
    }
}
