using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using System.Web.Security;
using TrackMyMpg.Models;

[assembly: PreApplicationStartMethod(typeof(PreApplicationTasks), "Initializer")]

public static class PreApplicationTasks
{
    public static void Initializer()
    {
        Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility
            .RegisterModule(typeof(AdminUserInitializationModule));
    }
}

public class AdminUserInitializationModule : IHttpModule
{
    private static bool initialized;
    private static object lockObject = new object();

    private const string adminEmail = "admin@trackmympg.com";
    private const string adminName = "admin";
    private const string adminPassword = "password";
    private const string adminRole = "Administrator";

    void IHttpModule.Init(HttpApplication context)
    {
        lock (lockObject)
        {
            if (!initialized)
            {
                var dbContext = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));

                //new InitializeSimpleMembershipAttribute().OnActionExecuting(null);

                if (userManager.FindByEmail(adminEmail) == null) {
                    userManager.Create(new ApplicationUser { UserName = adminName, Email = adminEmail }, adminPassword);
                }

                if(!roleManager.RoleExists(adminRole)) {
                    roleManager.Create(new IdentityRole(adminRole));
                }

                var user = userManager.FindByEmail(adminEmail);
                if(!userManager.IsInRole(user.Id, adminRole))
                {
                    userManager.AddToRole(user.Id, adminRole);
                }
            }
            initialized = true;
        }
    }

    void IHttpModule.Dispose() { }
}