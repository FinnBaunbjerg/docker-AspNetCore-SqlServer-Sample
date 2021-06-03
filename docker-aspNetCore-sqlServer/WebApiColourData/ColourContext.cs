using Microsoft.EntityFrameworkCore;

namespace WebApiColourData
{
    public class ColourContext : DbContext
    {
       public DbSet<Colour> Colours { get; set; }

       public ColourContext( DbContextOptions<ColourContext> options) : base(options)
       {
           
       }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColourEntityTypeConfiguration).Assembly);
           // new ColourEntityTypeConfiguration().Configure( modelBuilder.Entity<Colour>());
       }
    }
}