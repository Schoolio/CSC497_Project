using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CSC497_Project_JagQuiz.Models;

namespace CSC497_Project_JagQuiz.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationUserManager _userManager;

    }
}