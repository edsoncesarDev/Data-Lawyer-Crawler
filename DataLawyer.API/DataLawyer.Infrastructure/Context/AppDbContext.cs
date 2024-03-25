
using DataLawyer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLawyer.Infrastructure.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Process> Processes { get; set; }
    public DbSet<Movement> Movements { get; set; }
    public DbSet<Area> Areas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Process>().HasMany(x => x.Movements).WithOne().OnDelete(DeleteBehavior.Cascade);

    }

}
