using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Data.Entity.Core.Objects;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Net.Mail;

namespace CSC497_Project_JagQuiz.Models
{

    public class AccountIndexViewModel
    {
        public List<Course> courses { get; set; }
        public AccountIndexViewModel(List<string> inputCourses, UserManager userManager)
        {
            this.courses = new List<Course>();

            foreach (string item in inputCourses)
            {
                this.courses.Add(new Course(item, userManager.dbContext.uspGetTermsByUser(userManager.appUser.Email).ToList<uspGetTermsByUser_Result>().FindAll(p => p.CourseDscpt == item)));
            }
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
