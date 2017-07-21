using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EGuidebook.Models;
using System.Web.Security;
using EGuidebook.Shared;

namespace EGuidebook.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel objChangePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();

                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .FirstOrDefault(x => x.UserName.Equals(objChangePasswordViewModel.Username));

                if (objApplicationUser != null)
                {
                    var loginResult = await SignInManager.PasswordSignInAsync(objChangePasswordViewModel.Username, objChangePasswordViewModel.TempPassword, true, shouldLockout: false);
                    switch (loginResult)
                    {
                        case SignInStatus.Success:
                            var token = await UserManager.GeneratePasswordResetTokenAsync(objApplicationUser.Id.ToString());
                            var result = await UserManager.ResetPasswordAsync(objApplicationUser.Id.ToString(), token, objChangePasswordViewModel.NewPassword);

                            if (result.Succeeded)
                            {
                                objApplicationUser.IsPasswordChangeEnforced = false;
                                objApplicationDbContext.SaveChanges();
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                return RedirectToAction("Login");
                            }

                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode");
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("TempPassword", "Podane hasło tymczasowe jest nieprawidłowe");
                            return View(objChangePasswordViewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("Username", "Podany adres e-mail jest nieprawidłowy");
                }
            }
            return View(objChangePasswordViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(LoginViewModel objLoginViewModel)
        {
            if (ModelState.IsValidField("PasswordResetEmail"))
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .Where(x => x.UserName.Equals(objLoginViewModel.PasswordResetEmail))
                                                        .FirstOrDefault();
                if (objApplicationUser != null)
                {
                    string strNewPassword = Helper.GenerateRandomPassword(8, 1, 1, 1);
                    var token = await UserManager.GeneratePasswordResetTokenAsync(objApplicationUser.Id.ToString());
                    var result = await UserManager.ResetPasswordAsync(objApplicationUser.Id.ToString(), token, strNewPassword);

                    if (result.Succeeded)
                    {
                        try
                        {
                            SMTPManager.SendResetPasswordEmail(strNewPassword, objApplicationUser.Email);
                            objApplicationUser.IsPasswordChangeEnforced = true;
                            objApplicationDbContext.SaveChanges();
                            TempData["ShowResetPasswordAlert"] = "true";
                        }
                        catch (Exception ex) { }
                    }
                }
            }

            if (ModelState.ContainsKey("RegisterEmail")) ModelState["RegisterEmail"].Errors.Clear();
            if (ModelState.ContainsKey("RegisterPassword")) ModelState["RegisterPassword"].Errors.Clear();
            if (ModelState.ContainsKey("RegisterPasswordRepeat")) ModelState["RegisterPasswordRepeat"].Errors.Clear();
            if (ModelState.ContainsKey("LoginEmail")) ModelState["LoginEmail"].Errors.Clear();
            if (ModelState.ContainsKey("LoginPassword")) ModelState["LoginPassword"].Errors.Clear();

            TempData["ViewData"] = ViewData;
            TempData["ShowResetPasswordRow"] = true;
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel objLoginViewModel, string returnUrl)
        {
            if (ModelState.IsValidField("LoginEmail") &&
                ModelState.IsValidField("LoginPassword"))
            {
                var result = await SignInManager.PasswordSignInAsync(objLoginViewModel.LoginEmail, objLoginViewModel.LoginPassword, true, shouldLockout: false);

                if (ModelState.ContainsKey("RegisterEmail")) ModelState["RegisterEmail"].Errors.Clear();
                if (ModelState.ContainsKey("RegisterPassword")) ModelState["RegisterPassword"].Errors.Clear();
                if (ModelState.ContainsKey("RegisterPasswordRepeat")) ModelState["RegisterPasswordRepeat"].Errors.Clear();
                if (ModelState.ContainsKey("PasswordResetEmail")) ModelState["PasswordResetEmail"].Errors.Clear();

                switch (result)
                {
                    case SignInStatus.Success:

                        ApplicationUser objApplicationUser = new ApplicationDbContext()
                            .Users
                            .Where(x => x.UserName.Equals(objLoginViewModel.LoginEmail))
                            .FirstOrDefault();

                        if (objApplicationUser.IsPasswordChangeEnforced)
                        {
                            FormsAuthentication.SignOut();
                            return RedirectToAction("ChangePassword");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = true });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("LoginPassword", "Podany adres e-mail lub hasło jest nieprawidłowe");
                        return View(objLoginViewModel);
                }
            }

            return View(objLoginViewModel);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(LoginViewModel objLoginViewModel)
        {
            if (ModelState.IsValidField("RegisterEmail") &&
                ModelState.IsValidField("RegisterPassword") &&
                ModelState.IsValidField("RegisterPasswordRepeat"))
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                ApplicationUser objApplicationUser = objApplicationDbContext
                                                        .Users
                                                        .Where(x => x.Email.Equals(objLoginViewModel.RegisterEmail) ||
                                                                    x.UserName.Equals(objLoginViewModel.RegisterEmail))
                                                        .FirstOrDefault();

                if (objApplicationUser == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = objLoginViewModel.RegisterEmail,
                        Email = objLoginViewModel.RegisterEmail,
                    };
                    var result = await UserManager.CreateAsync(user, objLoginViewModel.RegisterPassword);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }
                else
                {
                    ModelState.AddModelError("RegisterEmail", "Podany adres e-mail już istnieje");
                }
            }

            if (ModelState.ContainsKey("LoginEmail")) ModelState["LoginEmail"].Errors.Clear();
            if (ModelState.ContainsKey("LoginPassword")) ModelState["LoginPassword"].Errors.Clear();
            if (ModelState.ContainsKey("PasswordResetEmail")) ModelState["PasswordResetEmail"].Errors.Clear();

            TempData["ViewData"] = ViewData;

            return RedirectToAction("Login");
        }        

        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}