

namespace Backend.LIB.Models;

public class LauncherContext : IdentityDbContext<BaseUser>
{
    
    public LauncherContext(DbContextOptions<LauncherContext> options) : base(options) { }

    public virtual DbSet<BaseUser> BaseUsers { get; set; }
    public virtual DbSet<BaseRole> BaseRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
