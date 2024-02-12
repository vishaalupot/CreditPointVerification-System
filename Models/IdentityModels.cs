using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CPV_Mark3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string UserRole { get; set; }
        public string Status { get; set; }

        public string EmpCode { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            //userIdentity.AddClaim(new Claim("FullName", FullName ?? ""));
            //userIdentity.AddClaim(new Claim("UserRole", UserRole ?? ""));
            //userIdentity.AddClaim(new Claim("Status", Status ?? ""));
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

        public System.Data.Entity.DbSet<CPV_Mark3.Models.ManageRoles> ManageRoles { get; set; }

        public System.Data.Entity.DbSet<CPV_Mark3.Models.UserRolesViewModel> UserRolesViewModels { get; set; }
    }
}