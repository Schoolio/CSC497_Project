using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CSC497_Project_JagQuiz.Models;

namespace CSC497_Project_JagQuiz.Controllers
{
    public class ClosedController : Controller
    {
        protected ApplicationUserManager ApplicationUserManager = new ApplicationUserManager();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            AppUserState localAppUserState = new AppUserState();

            if (User is ClaimsPrincipal)
            {
                var user = User as ClaimsPrincipal;
                var claims = user.Claims.ToList();

                var localAppUserStateString = GetClaim(claims, "ActiveUserState");

                if (!String.IsNullOrEmpty(localAppUserStateString))
                {
                    localAppUserState.Deserialize(localAppUserStateString);
                }
            }

            ApplicationUserManager.ActiveUserState = localAppUserState;
        }

        public static string GetClaim(List<Claim> claims, string key)
        {
            var claim = claims.FirstOrDefault(c => c.Type == key);
            if (claim == null)
                return null;

            return claim.Value;
        }

        public string BuildToken()
        {
            return null;
        }
    }
}