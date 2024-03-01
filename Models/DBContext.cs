using Microsoft.EntityFrameworkCore;
using ServerAPI_NCF.Models;

namespace ServerAPI_NCF.Models;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

public DbSet<ServerAPI_NCF.Models.UserItem> UserItem { get; set; } = default!;

    
}