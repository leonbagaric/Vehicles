using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Extensions.DependencyInjection;
using Vehicles.Interface;
using Vehicles.Models;
using Vehicles.Others;
using Vehicles.Service;

namespace Vehicles
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseServiceProviderFactory(new NinjectServiceProviderFactory());

            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleDB")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            builder.Host.ConfigureContainer<IKernel>(kernel =>
            {
                kernel.Load(new NinjectProfile());
            });

            builder.Services.AddScoped<IVehicleService, VehicleService>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await context.Database.EnsureCreatedAsync();

                await context.Database.MigrateAsync();
                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole("Admin"));

                var adminUser = await userManager.FindByEmailAsync("admin@test.com");
                if (adminUser == null)
                {
                    adminUser = new IdentityUser { UserName = "admin@test.com", Email = "admin@test.com", EmailConfirmed = true };
                    await userManager.CreateAsync(adminUser, "Test123.");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                    await StartupSeed.SeedAsync(context, userManager, roleManager);
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Vehicle}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}
