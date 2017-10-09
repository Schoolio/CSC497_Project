using CSC497_Project_JagQuiz.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;

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
                if (!String.IsNullOrEmpty(local.UserName))
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
            claims.Add(new Claim(ClaimTypes.NameIdentifier, ActiveUserState.UserName));
            claims.Add(new Claim(ClaimTypes.Name, ActiveUserState.UserName));

            //My serialized AppUserState object
            claims.Add(new Claim("ActiveUserState", ActiveUserState.Serialize()));

            //Assigning the claims to an identity object
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            //Assigning Authentication Properties
            AuthenticationProperties myAuthenProps = new AuthenticationProperties() { AllowRefresh = true, IsPersistent = false, ExpiresUtc = DateTime.UtcNow.AddDays(7) };

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

    }
    #endregion

    #region My Active User Absract class and child classes
    //My Active User Class
    //Holds information about the current user
    public class AppUserState
    {
        public string UserId = string.Empty;
        public string UserName = string.Empty;
        public string FirstName = string.Empty;
        public string LastName = string.Empty;

        public virtual string Serialize()
        {
            return String.Join("|", new string[] { this.FirstName, this.LastName, this.UserName, this.UserId });
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

            this.FirstName = strings[0];
            this.LastName = strings[1];
            this.UserName = strings[2];
            this.UserId = strings[3];

            return true;
        }

        public static AppUserState BuildAccount(uspLogIn_Result input)
        {
            if (input.AccountType == 1)
            {
                Admin output = new Admin();
                output.FirstName = input.FirstName;
                output.LastName = input.LastName;
                output.UserName = input.Email;
                return output;
            }

            else
            {
                Student output = new Student();
                output.FirstName = input.FirstName;
                output.LastName = input.LastName;
                output.UserName = input.Email;
                return output; ;
            }

        }

        public AccountIndexViewModel toAccountIndexModel()
        {
            AccountIndexViewModel output = new AccountIndexViewModel();
            output.email = UserName;
            output.firstName = FirstName;
            output.lastName = LastName;
            return output;
        }

        public bool isEmpty()
        {
            if (string.IsNullOrEmpty(this.UserId) || string.IsNullOrEmpty(this.UserName))
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

    }

    #endregion
}
