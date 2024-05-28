using Microsoft.EntityFrameworkCore;

namespace ReportService.Api.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    public DbSet<Domain.Report> Reports { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}