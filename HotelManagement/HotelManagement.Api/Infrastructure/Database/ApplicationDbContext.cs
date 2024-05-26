using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Api.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    

    public DbSet<Domain.Hotel> Hotels { get; set; }
    public DbSet<Domain.ContactInformation> ContactInformations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Hotel>()
            .HasMany(h => h.ContanInformation)
            .WithOne(c => c.Hotel)
            .HasForeignKey(c => c.HotelId);

        modelBuilder.Entity<Domain.ContactInformation>()
            .Property(c => c.InfoType)
            .HasConversion<string>();
    }
}