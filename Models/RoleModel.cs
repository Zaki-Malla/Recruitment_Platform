using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Recruitment_Platform.Models
{
    public class RoleModel : IdentityRole<int>
    {
        public RoleModel() : base() { }

        public RoleModel(string roleName) : base(roleName) { }
    }
}
