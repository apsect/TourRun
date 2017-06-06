using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TourRun.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "manager" };
            var role3 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            var admin = new ApplicationUser { Email = "admin@example.com", UserName = "admin@example.com" };
            string password = "1234567890";
            var result1 = userManager.Create(admin, password);

            var manager = new ApplicationUser { Email = "manager@example.com", UserName = "manager@example.com" };
            var result2 = userManager.Create(manager, password);

            if (result1.Succeeded && result2.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(admin.Id, role3.Name);

                userManager.AddToRole(manager.Id, role2.Name);
                userManager.AddToRole(manager.Id, role3.Name);
            }
            base.Seed(context);
        }
    }
}