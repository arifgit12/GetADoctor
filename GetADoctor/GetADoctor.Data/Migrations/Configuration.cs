using GetADoctor.Data.Infrastructure;
using GetADoctor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Migrations
{
    public sealed class Configuration : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            foreach (UserRoles role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!context.Roles.Any(r => r.Name == role.ToString()))
                {
                    var store = new RoleStore<IdentityRole>(context);
                    var manager = new RoleManager<IdentityRole>(store);
                    var newRole = new IdentityRole { Name = role.ToString() };
                    manager.Create(newRole);
                }
            }

            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    PhoneNumber = "9518557397"
                };

                manager.Create(user, "Admin@123");
                manager.AddToRole(user.Id, UserRoles.Administrator.ToString());
            }
        }
    }
}
