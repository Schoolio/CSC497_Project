using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSC497_Project_JagQuiz.Models
{
    public struct Student
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public List<Term> terms { get; set; }
        public int? score { get; set; }

        public Student(uspGetStudents_Result input, List<uspGetTermsByUser_Result> inputTerms)
        {
            this = new Student();
            this.terms = new List<Term>();
            this.firstName = input.firstName;
            this.lastName = input.lastName;
            this.email = input.email;

            foreach (var item in inputTerms)
            {
                this.terms.Add(new Term(item));
            }
        }

        public Student(uspGetStudents_Result input)
        {
            this = new Student();
            this.terms = new List<Term>();
            this.firstName = input.firstName;
            this.lastName = input.lastName;
            this.email = input.email;
            this.score = input.Average;
        }
    }

    public struct Term
    {
        public string termName { get; set; }
        public string definition { get; set; }
        public string module { get; set; }
        public string course { get; set; }
        public int? score { get; set; }

        public Term(uspGetTermsByUser_Result input)
        {
            this = new Term();
            this.termName = input.Term;
            this.score = input.Score;
        }

        public Term(uspGetTerm_Result input)
        {
            this = new Term();
            this.termName = input.Term;
            this.definition = input.Def;
        }

        public Term(uspGetTermsByModule_Result input)
        {
            this = new Term();
            this.termName = input.Term;
            this.definition = input.Def;
        }

        public Term(uspGetTermByCourse_Result input)
        {
            this = new Term();
            this.termName = input.Term;
            this.module = input.Module;
            this.definition = input.Def;
            this.score = input.Average;
        }
    }

    public struct Course
    {
        public string courseName { get; set; }
        public List<Student> students { get; set; }
        public List<Term> terms { get; set; }

        public Course(string inputCourse, List<uspGetStudents_Result> studentInput, List<uspGetTermByCourse_Result> termInput)
        {
            this = new Course();
            this.students = new List<Student>();
            this.terms = new List<Term>();
            this.courseName = inputCourse;

            foreach (var item in studentInput)
            {
                this.students.Add(new Student(item));
            }

            foreach (var local in termInput)
            {
                this.terms.Add(new Term(local));
            }
        }

        public Course(string inputCourse, List<uspGetTermsByUser_Result> termInput)
        {
            this = new Course();
            this.terms = new List<Term>();
            this.courseName = inputCourse;

            foreach (var local in termInput)
            {
                this.terms.Add(new Term(local));
            }
        }
    }


    public class AccountInformation
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string accountType { get; set; }
        public AccountInformation()
        {

        }
        public AccountInformation(AppUser appUser)
        {
            this.firstName = appUser.FirstName;
            this.lastName = appUser.LastName;
            this.email = appUser.Email;
        }
    }

    public class PasswordConfirmation
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}