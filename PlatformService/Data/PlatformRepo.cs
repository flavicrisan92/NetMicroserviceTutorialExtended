using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDbContext _context;

    public PlatformRepo(AppDbContext context)
    {
        _context = context;
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform == null)
        {
            throw new ArgumentNullException(nameof(platform));
        }
        _context.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatofrms() => _context.Platforms.ToList();

    public Platform GetPlatformById(int id) => _context.Platforms.FirstOrDefault(q => q.Id == id);

    public bool SaveChanges() => (_context.SaveChanges() >= 0);
}