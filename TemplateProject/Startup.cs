using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TemplateProject.Models;

[assembly: OwinStartupAttribute(typeof(TemplateProject.Startup))]
namespace TemplateProject
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRoles();
            createUsers();
        }
        public void createUsers()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.Email = "publisher@yahoo.com";
            user.UserName= "publisher@yahoo.com";
            var check = userManager.Create(user,"Asd!23");
            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Publishers");
            }
            var user1 = new ApplicationUser();
            user1.Email = "Appliers@yahoo.com";
            user1.UserName = "Appliers@yahoo.com";
             check = userManager.Create(user1, "Asd!23");
            if (check.Succeeded)
            {
                userManager.AddToRole(user1.Id, "Appliers");
            }
            var user2 = new ApplicationUser();
            user2.Email = "Admins@yahoo.com";
            user2.UserName = "Admins@yahoo.com";
            check = userManager.Create(user2, "Asd!23");
            if (check.Succeeded)
            {
                userManager.AddToRole(user2.Id, "Admins");
            }

        }
        public void createRoles()
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!roleManager.RoleExists("Admins"))
            {
                role = new IdentityRole();
                role.Name = "Admins";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Appliers"))
            {
                role = new IdentityRole();
                role.Name = "Appliers";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Publishers"))
            {
                role = new IdentityRole();
                role.Name = "Publishers";
                roleManager.Create(role);
            }
        }

    }
}
