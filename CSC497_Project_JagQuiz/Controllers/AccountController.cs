using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CSC497_Project_JagQuiz.Models;
using System.Collections.Generic;

namespace CSC497_Project_JagQuiz.Controllers
{
    [Authorize]
    public class AccountController : ClosedController
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            userManager.IdentitySignOut();
            if (userManager.ValidateUser(model))
            {
                userManager.IdentitySignIn(model);
               return Redirect("AccountIndex");
            }
            else
            {
                return View("Login");
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            string test = userManager.dbContext.uspRegisterUser(model.Email, model.Password, model.FirstName, model.LastName, 0).First();
            return View("Login");
            // If we got this far, something failed, redisplay form
            //return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        
        //
        // GET: /Account/AccountIndex
        public ActionResult AccountIndex()
        {
            AccountIndexViewModel local = new AccountIndexViewModel(userManager.dbContext.uspGetCourseByUser(userManager.appUser.AccountID).ToList(), userManager);
            return View(local);
        }

        public ActionResult AccountOptions()
        {
            AccountOptionsViewModel local = new AccountOptionsViewModel(userManager.appUser, userManager.dbContext.uspGetCourseByUser(userManager.appUser.AccountID).ToList(), userManager.dbContext.uspGetAllCourses().ToList());
            return View(local);
        }

        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(AccountOptionsViewModel model)
        {
            userManager.dbContext.uspChangePassword(model.accountInformation.email, model.passwordConfirmation.ConfirmPassword);
            return Redirect("AccountOptions");
        }

        public ActionResult RegisterCourse(AccountOptionsViewModel model)
        {
            userManager.dbContext.uspAddToCourse(userManager.appUser.Email, model.courseManagement.selectedCourse);
            return Redirect("AccountOptions");
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