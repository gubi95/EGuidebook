using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using EGuidebook.Models;
using System.Net.Http;
using System.Web.Http.Controllers;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Threading;

namespace EGuidebook.Areas.WebAPI
{
    public class WebAPIBasicAuth : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                // get header
                string strAuthroizationHeader = actionContext.Request.Headers.Authorization.Parameter.Replace("Basic ", "");
                // decode header
                byte[] arrByte = Convert.FromBase64String(strAuthroizationHeader);                
                string strCredentialsDecoded = System.Text.Encoding.UTF8.GetString(arrByte);
                // get username and password
                string strUsername = strCredentialsDecoded.Split(':')[0];
                string strPassword = strCredentialsDecoded.Split(':')[1];

                // get user from DB
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .FirstOrDefault(x => x.UserName.Equals(strUsername));                
                // check if password from request matches password in DB
                if (objApplicationUser == null || 
                    userManager.PasswordHasher.VerifyHashedPassword(objApplicationUser.PasswordHash, strPassword) != Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
                {
                    HandleUnauthorizedRequest(actionContext);
                }
                else
                {
                    IPrincipal principal = new GenericPrincipal(new GenericIdentity(objApplicationUser.UserName), new string[] { });
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                }
            }
            catch (Exception ex)
            {
                HandleUnauthorizedRequest(actionContext);                
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            HttpResponseMessage objHttpResponseMessage = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Content = new StringContent(JsonConvert.SerializeObject(new WebAPIResponse(false, WebAPIResponse.EnumWebAPIResponseCode.UNAUTHORIZED_ACCESS)))
            };
            objHttpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            actionContext.Response = objHttpResponseMessage;
        }
    }
}