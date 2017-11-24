using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSC497_Project_JagQuiz.Models
{

    public class AccountIndexViewModel
    {
        public List<string> courses { get; set; }
        public AppUser appUser { get; set; }
        public AccountIndexViewModel(AppUser appUser, List<string> courses)
        {
            this.appUser = appUser;
            this.courses = courses;
        }

    }

    public class AccountOptionsViewModel
    {
        public AccountInformation accountInformation { get; set; }
        public AccountCourseManagement courseManagement { get; set; }
        public PasswordConfirmation passwordConfirmation { get; set; }
        public AccountOptionsViewModel(AppUser appUser, List<string> courses, List<string> allCourses)
        {
            this.accountInformation = new AccountInformation(appUser);
            this.courseManagement = new AccountCourseManagement();
            this.passwordConfirmation = new PasswordConfirmation();
            this.courseManagement.allCourses = allCourses;
            this.courseManagement.courses = courses;
         }

        public AccountOptionsViewModel()
        {
            this.accountInformation = new AccountInformation();
            this.courseManagement = new AccountCourseManagement();
            this.passwordConfirmation = new PasswordConfirmation();
        }

        public class AccountCourseManagement
        {
            public List<string> courses { get; set; }
            public List<string> allCourses { get; set; }
            public string coursePassword { get; set; }
            public string selectedCourse { get; set; }

        }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

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
