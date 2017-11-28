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
    public class ManagementViewModel
    {
        public CourseManagement courseManagement { get; set; }
        public AccountManagement accountManagement { get; set; }
        public TermManagement termManagement { get; set; }
        

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
            public string activeCourse { get; set; }
            public List<Course> courses { get; set; }
            [Display(Name = "Input a Course")]
            public string inputCourse { get; set; }
            [Display(Name = "Change Course Name")]
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
            [Display(Name = "Term")]
            public string singleTerm { get; set; }
            [Display(Name = "Definition")]
            public string singleDef { get; set; }
            [Display(Name = "Module")]
            public string module { get; set; }
        }
    }
}