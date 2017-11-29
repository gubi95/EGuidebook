using EGuidebook.Areas.WebAPI;
using EGuidebook.Models;
using EGuidebook.Shared;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace EGuidebook.Areas.WebAPI.Controllers
{    
    public class UserController : ApiController
    {
        public class LoginResponse : WebAPIResponse
        {
            public List<SpotCategoryModel> SpotCategories { get; private set; }
            public int RoutesCount { get; private set; }

            public LoginResponse(bool bSuccess, EnumWebAPIResponseCode eCode, List<SpotCategoryModel> listSpotCategoryModel, int nRoutesCount = -1) : base(bSuccess, eCode)
            {
                this.SpotCategories = listSpotCategoryModel;
                this.RoutesCount = nRoutesCount;
            }
        }

        [WebAPIBasicAuth]
        [HttpPost]
        public LoginResponse Login()
        {
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                List<SpotCategoryModel> listSpotCategoryModel = objApplicationDbContext
                                                                    .SpotCategories
                                                                    .ToList();

                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .FirstOrDefault(x => x.UserName.Equals(HttpContext.Current.User.Identity.Name));

                int nRoutesCount = objApplicationDbContext
                                    .Routes
                                    .Count(x => x.IsSystemRoute || ("" + x.CreatedByUserID).ToLower().Equals(objApplicationUser.Id.ToLower()));

                return new LoginResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK, listSpotCategoryModel, nRoutesCount);
            }
            catch (Exception ex) { }
            return new LoginResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR, null);
        }

        public class RegisterData
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private bool validatePassword(string strPassword)
        {
            return
                !string.IsNullOrEmpty(strPassword) &&                       // is empty
                strPassword.Length >= 6 &&                                  // at least 6 characters
                strPassword.Any(x => Char.IsLetterOrDigit(x)) &&            // at least 1 alphanumeric
                strPassword.Any(x => !Char.IsLetterOrDigit(x)) &&           // at least 1 nonalphanumeric
                strPassword.Any(x => Char.IsUpper(x));                      // at least 1 uppercase
        }

        [HttpPost]        
        public async System.Threading.Tasks.Task<LoginResponse> Register(RegisterData objRegisterData)
        {
            try
            {
                bool bIsEmailValid = !string.IsNullOrEmpty(objRegisterData.Username);
                try
                {
                    MailAddress objMailAddress = new MailAddress(objRegisterData.Username);
                }
                catch (Exception ex)
                {
                    bIsEmailValid = false;
                }

                if (!bIsEmailValid)
                {
                    return new LoginResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_USERNAME, new List<SpotCategoryModel>());
                }

                if (!this.validatePassword(objRegisterData.Password))
                {
                    return new LoginResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INCORRECT_PASSWORD, new List<SpotCategoryModel>());
                }

                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                ApplicationUser objApplicationUserExists = objApplicationDbContext
                                                                .Users
                                                                .FirstOrDefault(x => x.UserName.Equals(objRegisterData.Username));

                if (objApplicationUserExists != null)
                {
                    return new LoginResponse(false, WebAPIResponse.EnumWebAPIResponseCode.USER_ALREADY_EXISTS, new List<SpotCategoryModel>());
                }
                else
                {
                    ApplicationUser objApplicationUser = new ApplicationUser()
                    {
                        Email = objRegisterData.Username,
                        IsPasswordChangeEnforced = false,
                        UserName = objRegisterData.Username,
                        AvatarImagePath = null,
                        FirstName = null,
                        LastName = null
                    };

                    var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var result = await userManager.CreateAsync(objApplicationUser, objRegisterData.Password);

                    if (result.Succeeded)
                    {
                        IdentityRole objIdentityRole = objApplicationDbContext
                                                        .Roles
                                                        .FirstOrDefault(x => x.Id.Equals("MBL"));
                        if (objIdentityRole != null)
                        {
                            await userManager.AddToRoleAsync(objApplicationUser.Id, objIdentityRole.Name);

                            List<SpotCategoryModel> listSpotCategoryModel = objApplicationDbContext
                                                                    .SpotCategories
                                                                    .ToList();

                            int nRoutesCount = objApplicationDbContext
                                                .Routes
                                                .Count(x => x.IsSystemRoute);

                            return new LoginResponse(true, WebAPIResponse.EnumWebAPIResponseCode.OK, listSpotCategoryModel, nRoutesCount);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return new LoginResponse(false, WebAPIResponse.EnumWebAPIResponseCode.INTERNAL_SERVER_ERROR, new List<SpotCategoryModel>());
        }
    }
}
