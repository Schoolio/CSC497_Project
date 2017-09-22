using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CSC497_Project_JagQuiz.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string JagNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccountType { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public int RegisterQuery(RegisterViewModel MyParams){
            object[] SQLParams = new object[6];
            SQLParams[0] = MyParams.JagNumber;
            SQLParams[1] = MyParams.Email;
            SQLParams[2] = MyParams.Password;
            SQLParams[3] = MyParams.FirstName;
            SQLParams[4] = MyParams.LastName;
            SQLParams[5] = 1;
            object[] SQLParam = new object[6];
            SQLParam[0] = "J00426100";
            SQLParam[1] = "zps1101@jagmail.southalabama.edu";
            SQLParam[2] = "j4c0bi4n&k4ns4r";
            SQLParam[3] = "Zac";
            SQLParam[4] = "Smith";
            SQLParam[5] = 1;
            int response = Database.ExecuteSqlCommand("EXEC uspRegisterUser  @pJagNumber='J00426100', @pEmail='temp', @pPasswordHash='temp', @pFirstName='temp', @pLastName='temp', @pAccountType=1", SQLParam);
            return response;
        }
    }
}