using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UserAuthIdentityApi.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UserAuthIdentityApi.Settings;

[assembly: HostingStartup(typeof(UserAuthIdentityApi.Areas.Identity.IdentityHostingStartup))]
namespace UserAuthIdentityApi.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup  //We used this file for data migration to PostgreSql.
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetSection(nameof(PostgresqlSettings)).Get<PostgresqlSettings>().ConnectionString));
               /* services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("PostgresqlAuthConnection")));*/
                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            });

        }
    }
}
