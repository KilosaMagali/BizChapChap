using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BizChapChap.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Entity Sets
        public IDbSet<Listing> Listing { get; set; }
        public IDbSet<Currency> Currency { get; set; }
        public IDbSet<Category> Category { get; set; }
        public IDbSet<SubCategory> SubCategory { get; set; }
        public IDbSet<Seller> Seller { get; set; }
        public IDbSet<SellerType> SellerType { get; set; }
        public IDbSet<Error> Error { get; set; }
        #endregion

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

    }
}