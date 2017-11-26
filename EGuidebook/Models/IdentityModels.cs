using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EGuidebook.Models
{    
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarImagePath { get; set; }
        public bool IsPasswordChangeEnforced { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<SpotModel> Spots { get; set; }
        public virtual DbSet<RouteModel> Routes { get; set; }
        public virtual DbSet<SpotGradeModel> Grades { get; set; }
        public virtual DbSet<SpotCategoryModel> SpotCategories { get; set; }
        public virtual DbSet<SystemSettingsModel> SystemSettings { get; set; }
        public virtual DbSet<SpotsRoutesModel> SpotsRoutes { get; set; }

        public ApplicationDbContext()
            : base("defConnString", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder objDbModelBuilder)
        {
            objDbModelBuilder.Entity<IdentityUserLogin>().HasKey<string>(x => x.UserId);
            objDbModelBuilder.Entity<IdentityRole>().HasKey<string>(x => x.Id);
            objDbModelBuilder.Entity<IdentityUserRole>().HasKey(x => new { x.RoleId, x.UserId });
            base.OnModelCreating(objDbModelBuilder);
        }
    }
}