using CommandsService.Models;

namespace CommandsService.Data;

public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _context;

    public CommandRepo(AppDbContext context)
    {
        _context = context;
    }

    public void CreateCommand(int platformId, Command command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        command.PlatformId = platformId;
        _context.Commands.Add(command);
    }

    public void CreatePlatform(Platform plat)
    {
        if (plat == null)
        {
            throw new ArgumentNullException(nameof(plat));
        }
        _context.Platforms.Add(plat);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public Command GetCommand(int platformId, int commandId)
    {
        return _context.Commands.Where(q => q.PlatformId == platformId && q.Id == commandId).FirstOrDefault();
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
        return _context.Commands.Where(q => q.PlatformId == platformId)
        .OrderBy(q => q.Platform.Name);
    }

    public bool PlatformExist(int platformId)
    {
        return _context.Platforms.Any(q => q.Id == platformId);
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }
}