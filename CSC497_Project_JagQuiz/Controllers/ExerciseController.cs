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
        public ActionResult Match()
        {
            AccountOptionsViewModel model = new AccountOptionsViewModel(userManager.appUser, userManager.dbContext.uspGetCourseByUser(userManager.appUser.AccountID).ToList(), userManager.dbContext.uspGetAllCourses().ToList());
            return View("Match", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Match(AccountOptionsViewModel model)
        {
            MultipleChoiceViewModel local = new MultipleChoiceViewModel(userManager.dbContext, model.courseManagement.selectedCourse, model.courseManagement.module);
            return View("MultipleChoiceQuestion", local);
        }

        public ActionResult MultipleChoice(MultipleChoiceViewModel model)
        {
            return View("MultipleChoiceQuestion", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Answer(MultipleChoiceViewModel model)
        {
            if (model.question.correctAnswer == model.userAnswer)
            {
                userManager.dbContext.uspUpdateScore(model.question.correctAnswer, userManager.appUser.Email, 4);
            }
            else
            {
                userManager.dbContext.uspUpdateScore(model.question.correctAnswer, userManager.appUser.Email, -1);
            }
            uspCreateMCQuestion_Result test = userManager.dbContext.uspCreateMCQuestion(model.activeCourse, model.module).First();
            MCQuestion newQ = new MCQuestion(test);
            model.question = newQ;
            return View("MultipleChoiceQuestion", model);
        }
    }
}