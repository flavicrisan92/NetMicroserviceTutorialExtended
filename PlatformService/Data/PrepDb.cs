namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder)
    {
        using (var servicesScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
        {
            SeedData(servicesScope.ServiceProvider.GetService<AppDbContext>());
        }
    }

    private static void SeedData(AppDbContext appDbContext)
    {
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