using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Net.Mail;
using System.Data.Entity.Core.Objects;


namespace CSC497_Project_JagQuiz.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ManagementViewModel
    {
        public CourseManagement courseManagement;
        public AccountManagement accountManagement;

        public ManagementViewModel(UserManager userManager)
        {
            this.courseManagement = new CourseManagement(userManager.dbContext.uspGetCourses(userManager.appUser.AccountID).ToList<string>(), userManager.dbContext.uspGetStudents(userManager.appUser.AccountID).ToList<uspGetStudents_Result>());
            this.accountManagement = new AccountManagement();
        }
        public class CourseManagement
        {
            public List<string> courses;
            public List<uspGetStudents_Result> students;
            public string newCourse { get; set; }

            public CourseManagement(List<string> courses, List<uspGetStudents_Result> students)
            {
                this.courses = courses;
                this.students = students;
            }
        }

        public class AccountManagement
        {
            public string searchParam;

            public AccountManagement()
            {
                this.searchParam = "";
            }
        }
    }
}