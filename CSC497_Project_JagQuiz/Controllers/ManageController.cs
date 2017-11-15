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
    public class ManageController : ClosedController
    {
        public ActionResult Management()
        {
            ManagementViewModel local = new ManagementViewModel(userManager);
            return View("Management", local);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse() 
        {
            
            return Redirect("Management");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse()
        {
            return Redirect("Management");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTerms()
        {
            return Redirect("Management");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTerms()
        {
            return Redirect("Management");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyTerm()
        {
            return Redirect("Management");
        }

        
    }
}