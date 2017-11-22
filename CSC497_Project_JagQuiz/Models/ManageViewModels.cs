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
        public CourseManagement courseManagement { get; set; }
        public AccountManagement accountManagement { get; set; }
        public TermManagement termManagement { get; set; }
        public string activeCourse { get; set; }

        public ManagementViewModel(UserManager userManager)
        {
            this.courseManagement = new CourseManagement(userManager);
            this.accountManagement = new AccountManagement();
            this.termManagement = new TermManagement();
        }

        public ManagementViewModel()
        {
            this.courseManagement = new CourseManagement();
            this.accountManagement = new AccountManagement();
            this.termManagement = new TermManagement();
        }
        public class CourseManagement
        {
            public List<Course> courses;
            public string inputCourse { get; set; }
            public string modifyCourse { get; set; }

            public CourseManagement(UserManager userManager)
            {
                this.courses = new List<Course>();
                List<string> localCourseList = userManager.dbContext.uspGetCoursesAsAdmin(userManager.appUser.Email).ToList<string>();

                foreach (string item in localCourseList)
                {
                    this.courses.Add(new Course(item, userManager.dbContext.uspGetStudents(item).ToList<uspGetStudents_Result>(), userManager.dbContext.uspGetTermByCourse(item).ToList<uspGetTermByCourse_Result>()));
                }
            }

            public CourseManagement()
            {
                this.inputCourse = string.Empty;

            }

            public class Course
            {
                public string course;
                public List<uspGetStudents_Result> students;
                public List<uspGetTermByCourse_Result> terms;

                public Course(string course, List<uspGetStudents_Result> students, List<uspGetTermByCourse_Result> terms)
                {
                    this.course = course;
                    this.students = students;
                    this.terms = terms;
                }
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

        public class TermManagement
        {
            public string singleTerm;
            public string singleDef;
            public string module;
        }
    }
}