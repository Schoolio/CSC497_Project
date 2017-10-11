using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}