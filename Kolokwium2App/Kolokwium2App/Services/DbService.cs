using Kolokwium2App.Data;

namespace Kolokwium2App.Services;

public class DbService : IDbService
{
    private readonly MyContext _context;

    public DbService(MyContext context)
    {
        _context = context;
    }

}