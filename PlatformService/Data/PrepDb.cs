using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder, bool isProduction)
    {
        Console.WriteLine($"--> isProd: {isProduction}");
        using (var servicesScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
        {
            SeedData(servicesScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
        }
    }

    private static void SeedData(AppDbContext appDbContext, bool isProduction)
    {
        if (isProduction)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                appDbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not run migrations: {ex.Message}");
            }
        }

        if (!appDbContext.Platforms.Any())
        {
            Console.WriteLine("--> Seeding data");
            appDbContext.Platforms.AddRange(new Models.Platform()
            {
                Name = "Dot net",
                Publisher = "Microsoft",
                Cost = "Free"
            },
            new Models.Platform()
            {
                Name = "Sql Server Express",
                Publisher = "Microsoft",
                Cost = "Free"
            },
            new Models.Platform()
            {
                Name = "Kubernetes",
                Publisher = "Cloud Native Computing Foundation",
                Cost = "Free"
            });

            appDbContext.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
}