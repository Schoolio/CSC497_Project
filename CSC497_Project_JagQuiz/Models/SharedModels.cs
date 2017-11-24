using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSC497_Project_JagQuiz.Models
{
    public struct student
    {
        string firstName;
        string lastName;
        string email;
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