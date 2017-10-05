using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGuidebook.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EGuidebook.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public RoleViewModel Role { get; set; }

        public UserViewModel() { }

        public UserViewModel(ApplicationUser objApplicationUser, List<IdentityRole> listIdentityRole = null)
        {
            this.UserId = objApplicationUser.Id;
            this.Username = objApplicationUser.UserName;

            if (listIdentityRole != null && listIdentityRole.Count > 0)
            {
                this.Role = new RoleViewModel(objApplicationUser.Roles.ToArray()[0], listIdentityRole);
            }
            else
            {
                this.Role = new RoleViewModel();
            }
        }
    }
}