using CSC497_Project_JagQuiz.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Net.Mail;

namespace CSC497_Project_JagQuiz
{
    // This class is used to control and manage the active user of the system. When the ActiveUser value is null it is assumed that no user is logged into the system.
    public class ApplicationUserManager : IDisposable
    {
        private CSC497_Account_Entity _db;
        public EmailService EmailTool;
        public AppUserState ActiveUserState = new AppUserState();

        //Creating the Authentication Manager that handles the creation and deletion of authentication cookies within the application
        private IAuthenticationManager AuthenticationManager
        {
            get { return System.Web.HttpContext.Current.GetOwinContext().Authentication; }
        }

        //Default Constructor
        public ApplicationUserManager()
        {
            _db = new CSC497_Account_Entity();
            EmailTool = new EmailService();
        }

        //Create method for use in OWIN
        public static ApplicationUserManager Create()
        {
            var manager = new ApplicationUserManager();

            return manager;
        }

        public bool ValidateUser(LoginViewModel model)
        {
            try
            {
                AppUserState local = AppUserState.BuildAccount(_db.uspLogIn(model.Email, model.Password).First());
                if (!String.IsNullOrEmpty(local.JagNumber))
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //Method to call the SQL Procedure to register the user
        public string Register(RegisterViewModel model)
        {
            var response = _db.uspRegisterUser(model.JagNumber, model.Email, model.Password, model.FirstName, model.LastName, 1);
            return response.ToString();
        }

        //Identity Helper Functions
        public void IdentitySignIn(LoginViewModel model)
        {
            ActiveUserState = AppUserState.BuildAccount(_db.uspLogIn(model.Email, model.Password).First());

            //Taking the Active user and assigned it to a list of claims
            var claims = new List<Claim>();

            //Required Claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, ActiveUserState.JagNumber));
            claims.Add(new Claim(ClaimTypes.Email, ActiveUserState.Email));

            //My serialized AppUserState object
            claims.Add(new Claim("ActiveUserState", ActiveUserState.Serialize()));

            //Assigning the claims to an identity object
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            //Assigning Authentication Properties
            AuthenticationProperties myAuthenProps = new AuthenticationProperties() { AllowRefresh = true, IsPersistent = false, ExpiresUtc = DateTime.UtcNow.AddMinutes(5) };

            //Creates the authentication cookie.
            AuthenticationManager.SignIn(myAuthenProps, identity);
        }

        public void IdentitySignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        #region Disposing Functionality
        //Disposing functionality
        protected bool Disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }
        #endregion
    }

    #region Email Service tools
    public class EmailService
    {
        SmtpClient client;
        
        public EmailService()
        {
            client = new SmtpClient("smtp.gmail.com", 465);
            client.Credentials = new NetworkCredential("JagMatch@southalabama.edu", "South2017");
        }

        public bool ForgotPasswordEmail(string target, string link)
        {
            try
            {
                MailMessage local = new MailMessage();
                local.From = new MailAddress("JagMatch@southalabama.edu");
                local.To.Add(target);
                local.Subject = "Password Reset Link";
                local.Subject = "Here is the link to reset your password: \n" + link;
                client.Send(local);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    #endregion

    #region My Active User class and child classes
    //My Active User Class
    //Holds information about the current user
    public class AppUserState
    {
        public string JagNumber = string.Empty;
        public string Email = string.Empty;
        public string FirstName = string.Empty;
        public string LastName = string.Empty;

        public virtual string Serialize()
        {
            return String.Join("|", new string[] { this.JagNumber, this.Email, this.FirstName, this.LastName});
        }

        public virtual bool Deserialize(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return false;
            }

            string[] strings = input.Split('|');

            if (strings.Length < 4)
            {
                return false;
            }

            this.JagNumber = strings[0];
            this.Email = strings[1];
            this.FirstName = strings[2];
            this.LastName = strings[3];

            return true;
        }

        public static AppUserState BuildAccount(uspLogIn_Result input)
        {
            if (input.AccountType == 1)
            {
                Admin output = new Admin();
                output.FirstName = input.FirstName;
                output.LastName = input.LastName;
                output.Email = input.Email;
                output.JagNumber = input.JagNumber;
                return output;
            }

            else
            {
                Student output = new Student();
                output.FirstName = input.FirstName;
                output.LastName = input.LastName;
                output.Email = input.Email;
                output.JagNumber = input.JagNumber;
                return output; ;
            }

        }

        public bool isEmpty()
        {
            if (string.IsNullOrEmpty(this.JagNumber) || string.IsNullOrEmpty(this.Email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Admin : AppUserState
    {

    }
    class Student : AppUserState
    {
        public List<string> Courses = new List<string>();
        //public List<uspGetTerms> Terms = new List<uspGetTerms>();
    }

    #endregion
}
