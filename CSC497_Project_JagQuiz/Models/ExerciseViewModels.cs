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
        public List<MCQuestion> questions { get; set; }

        public MultipleChoiceViewModel(Project_CSC497Entities db, string course, string module)
        {
            List<uspGetTermsByModule_Result> local = db.uspGetTermsByModule(course, module).ToList();
            int length = local.Count() - 1;
            Random rand = new Random();
            foreach (var item in local)
            {
                questions.Add(new MCQuestion(item.Def, item.Term, local.ElementAt(rand.Next(0, length)).Term, local.ElementAt(rand.Next(0, length)).Term, local.ElementAt(rand.Next(0, length)).Term));
            }
        }

        public struct MCQuestion{
            public string question;
            public string correctAnswer;
            public string answer1;
            public string answer2;
            public string answer3;

            public MCQuestion(string question, string correctAnswer, string answer1, string answer2, string answer3)
            {
                this.question = question;
                this.correctAnswer = correctAnswer;
                this.answer1 = answer1; 
                this.answer2 = answer2;
                this.answer3 = answer3;
            }
        }
    }
}