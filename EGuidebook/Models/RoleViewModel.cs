using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string Name { get; set; }

        public RoleViewModel()
        {
            this.RoleId = "";
            this.Name = "";
        }

        public RoleViewModel(IdentityRole objIdentityRole)
        {
            this.RoleId = objIdentityRole.Id;
            this.Name = objIdentityRole.Name;
        }

        public RoleViewModel(IdentityUserRole objIdentityUserRole, List<IdentityRole> listIdentityRole)
        {
            this.RoleId = objIdentityUserRole.RoleId;
            this.Name = listIdentityRole.Find(x => x.Id.Equals(this.RoleId)).Name;
        }
    }
}