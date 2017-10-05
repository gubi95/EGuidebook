using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGuidebook.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using EGuidebook.Shared;
using System.Data.Entity;

namespace EGuidebook.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

            List<IdentityRole> listIdentityRole = objApplicationDbContext
                                                    .Roles
                                                    .ToList();

            List<UserViewModel> listUserViewModel = objApplicationDbContext
                                                    .Users
                                                    .ToList()
                                                    .Select(x => new UserViewModel(x, listIdentityRole))
                                                    .ToList();
            return View(listUserViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<RoleViewModel> listRoleViewModel = new ApplicationDbContext()
                                                    .Roles
                                                    .ToList()
                                                    .Select(x => new RoleViewModel(x))
                                                    .ToList();

            return View(new EditUserViewModel(listRoleViewModel));
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(EditUserViewModel objEditUserViewModel, HttpPostedFileBase user_avatar)
        {
            ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                ApplicationUser objApplicationUserExists = objApplicationDbContext
                                                            .Users
                                                            .FirstOrDefault(x => x.UserName.Equals(objEditUserViewModel.Username));

                if (objApplicationUserExists != null)
                {
                    ModelState.AddModelError("Username", "Podany adres e-mail jest już zajęty");
                }
                else
                {
                    ApplicationUser objApplicationUser = new ApplicationUser()
                    {
                        Email = objEditUserViewModel.Username,
                        IsPasswordChangeEnforced = false,
                        UserName = objEditUserViewModel.Username,
                        AvatarImagePath = Helper.SaveUserAvatar(user_avatar),
                        FirstName = objEditUserViewModel.FirstName,
                        LastName = objEditUserViewModel.LastName
                    };

                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var result =
                        objEditUserViewModel.RoleId.Equals("ADM") ?
                            await userManager.CreateAsync(objApplicationUser, objEditUserViewModel.UserPassword) :
                            await userManager.CreateAsync(objApplicationUser);

                    if (result.Succeeded)
                    {
                        IdentityRole objIdentityRole = objApplicationDbContext
                                                        .Roles
                                                        .FirstOrDefault(x => x.Id.Equals(objEditUserViewModel.RoleId));
                        if (objIdentityRole != null)
                        {
                            await userManager.AddToRoleAsync(objApplicationUser.Id, objIdentityRole.Name);
                        }
                    }

                    return RedirectToAction("Edit", new { @id = objApplicationUser.Id });
                }
            }
            
            List<RoleViewModel> listRoleViewModel = objApplicationDbContext
                                                        .Roles
                                                        .ToList()
                                                        .Select(x => new RoleViewModel(x))
                                                        .ToList();

            objEditUserViewModel.Roles = listRoleViewModel;
            return View(objEditUserViewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            ApplicationUser objApplicationUser = new ApplicationDbContext()
                                                    .Users
                                                    .Include(x => x.Roles)
                                                    .FirstOrDefault(x => x.Id.Equals(id.ToString()));

            if(objApplicationUser != null)
            {
                List<RoleViewModel> listRoleViewModel = new ApplicationDbContext()
                                                        .Roles
                                                        .ToList()
                                                        .Select(x => new RoleViewModel(x))
                                                        .ToList();

                return View(new EditUserViewModel(listRoleViewModel, objApplicationUser));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(Guid id, EditUserViewModel objEditUserViewModel, HttpPostedFileBase user_avatar)
        {
            ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
            List<IdentityRole> listIdentityRole = objApplicationDbContext
                                                        .Roles
                                                        .ToList();

            List<RoleViewModel> listRoleViewModel = listIdentityRole
                                                        .Select(x => new RoleViewModel(x))
                                                        .ToList();

            objEditUserViewModel.Roles = listRoleViewModel;

            if (ModelState.IsValid)
            {
                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .Include(x => x.Roles)
                                                        .FirstOrDefault(x => x.Id.Equals(id.ToString()));

                if (objApplicationUser != null)
                {
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                    if (!objApplicationUser.Roles.ToList()[0].RoleId.Equals(objEditUserViewModel.RoleId))
                    {
                        IdentityRole objIdentityRoleOld = listIdentityRole.Find(x => x.Id.Equals(objApplicationUser.Roles.ToList()[0].RoleId));
                        IdentityRole objIdentityRoleNew = listIdentityRole.Find(x => x.Id.Equals(objEditUserViewModel.RoleId));
                        await userManager.RemoveFromRoleAsync(objApplicationUser.Id, objIdentityRoleOld.Name);
                        await userManager.AddToRoleAsync(objApplicationUser.Id, objIdentityRoleNew.Name);
                    }

                    objApplicationUser.FirstName = objEditUserViewModel.FirstName;
                    objApplicationUser.LastName = objEditUserViewModel.LastName;

                    if(!string.IsNullOrEmpty(objEditUserViewModel.AvatarImagePath) && user_avatar != null && user_avatar.ContentLength > 0)
                    {
                        if(Helper.CheckIfFileExists(objApplicationUser.AvatarImagePath))
                        {
                            Helper.DeleteFile(objApplicationUser.AvatarImagePath);
                        }

                        objApplicationUser.AvatarImagePath = Helper.SaveUserAvatar(user_avatar);
                    }
                    else if(string.IsNullOrEmpty(objEditUserViewModel.AvatarImagePath))
                    {
                        if (Helper.CheckIfFileExists(objApplicationUser.AvatarImagePath))
                        {
                            Helper.DeleteFile(objApplicationUser.AvatarImagePath);
                        }
                        objApplicationUser.AvatarImagePath = null;
                    }                    

                    if (!string.IsNullOrEmpty(objEditUserViewModel.UserPassword) &&
                        !string.IsNullOrEmpty(objEditUserViewModel.UserPasswordRepeat) &&
                        objEditUserViewModel.UserPassword.Equals(objEditUserViewModel.UserPasswordRepeat))
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(objApplicationUser.Id);
                        await userManager.ResetPasswordAsync(objApplicationUser.Id, token, objEditUserViewModel.UserPassword);
                    }

                    objApplicationDbContext.SaveChanges();
                }
            }
            return View(objEditUserViewModel);
        }
    }
}