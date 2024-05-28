using Microsoft.EntityFrameworkCore;

namespace ReportService.Api.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    public DbSet<Domain.Report> Reports { get; set; }
    public DbSet<Domain.ReportContent> ReportContents { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Report>()
            .HasMany(h => h.ReportContents)
            .WithOne(c => c.Report)
            .HasForeignKey(c => c.ReportId);

        modelBuilder.Entity<Domain.Report>()
            .Property(c => c.State)
            .HasConversion<string>();
    }
}