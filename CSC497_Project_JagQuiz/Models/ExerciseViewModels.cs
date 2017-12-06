using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSC497_Project_JagQuiz.Models
{
    public class ExerciseViewModels
    {
    }

    public class MultipleChoiceViewModel
    {
        public MCQuestion question { get; set; }
        public string userAnswer { get; set; }
        public string activeCourse { get; set; }
        public string module { get; set; }
        public MultipleChoiceViewModel()
        {

        }

        public MultipleChoiceViewModel(Project_CSC497Entities db, string course, string module)
        {
            this.activeCourse = course;
            this.module = module;
            this.userAnswer = "";
            this.question = new MCQuestion(db.uspCreateMCQuestion(course, module).First());

        }


    }
    public struct MCQuestion
    {
        public string question;
        public string correctAnswer { get; set; }
        public string[] answers;

        public MCQuestion(uspCreateMCQuestion_Result result) : this()
        {
            Random rnd = new Random();
            this.answers = new string[] { result.Answer1, result.Answer2, result.Answer3, result.Term };
            this.correctAnswer = result.Term;
            this.question = result.Def;
            this.answers = this.answers.OrderBy(x => rnd.Next()).ToArray();
        }
    }
}