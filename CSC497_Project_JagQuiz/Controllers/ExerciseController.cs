using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSC497_Project_JagQuiz.Models;

namespace CSC497_Project_JagQuiz.Controllers
{
    [Authorize]
    public class ExerciseController : ClosedController
    {
        // GET: Exercise
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Match()
        {
            return View();
        }

        public ActionResult MultipleChoice(string course, string module)
        {
            MultipleChoiceViewModel model = new MultipleChoiceViewModel(userManager.dbContext,course, module);
            return View("MultipleChoiceQuestion", model);
        }

        public ActionResult GetQuestion()
        {
            return null;
        }

        public ActionResult Answer(MultipleChoiceViewModel model)
        {
            return null;
        }
    }
}