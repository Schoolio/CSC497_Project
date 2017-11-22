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
            ManagementViewModel model = new ManagementViewModel(userManager);
            return View("Management", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse(ManagementViewModel model) 
        {
            userManager.dbContext.uspAddCourse(userManager.appUser.AccountID, model.courseManagement.inputCourse);
            return Redirect("Management");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse(ManagementViewModel model)
        {
            userManager.dbContext.uspDeleteCourse(model.courseManagement.inputCourse);
            return Redirect("Management");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyCourse(ManagementViewModel model)
        {
            userManager.dbContext.uspUpdateCourse(model.activeCourse, model.courseManagement.inputCourse);
            return Redirect("Management");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOneTerm(ManagementViewModel model)
        {
            userManager.dbContext.uspAddTerm(model.termManagement.singleTerm, model.termManagement.singleDef, model.termManagement.module, model.activeCourse);
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
            //userManager.dbContext.uspUpdateTerm()
            return Redirect("Management");
        }

        
    }
}