using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vehicles.Models;

namespace Vehicles.Others
{
    public class StartupSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await context.VehicleMakes.AnyAsync())
            {
                var makes = new List<VehicleMake>
        {
            new VehicleMake { Name = "Mercedes-Benz", Abrv = "MB" },
            new VehicleMake { Name = "Bayerische Motoren Werke", Abrv = "BMW" },
            new VehicleMake { Name = "VolksWagen", Abrv = "VW" },
            new VehicleMake { Name = "Rolls Royce", Abrv = "RR" },
        };

                await context.VehicleMakes.AddRangeAsync(makes);
                await context.SaveChangesAsync();

                var models = new List<VehicleModel>
        {
            new VehicleModel { Name = "G-63 Brabus", Abrv = "G-wagon", MakeId = makes.Single(m => m.Abrv == "MB").Id },
            new VehicleModel { Name = "CLS 63 AMG", Abrv = "CLS", MakeId = makes.Single(m => m.Abrv == "MB").Id },
            new VehicleModel { Name = "X5", Abrv = "X5", MakeId = makes.Single(m => m.Abrv == "BMW").Id },
            new VehicleModel { Name = "Golf 5", Abrv = "G 5", MakeId = makes.Single(m => m.Abrv == "VW").Id },
            new VehicleModel { Name = "Polo", Abrv = "Polo", MakeId = makes.Single(m => m.Abrv == "VW").Id },
            new VehicleModel { Name = "Ghost Series II", Abrv = "Ghost 2", MakeId = makes.Single(m => m.Abrv == "RR").Id },
        };

                await context.VehicleModels.AddRangeAsync(models);
                await context.SaveChangesAsync();
            }
        }
    }
}
