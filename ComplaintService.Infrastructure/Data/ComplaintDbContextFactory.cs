using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ComplaintService.Infrastructure.Data;

public class ComplaintDbContextFactory : IDesignTimeDbContextFactory<ComplaintDbContext>
{
    public ComplaintDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ComplaintDbContext>();

        // For EF CLI usage (local development)
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cms_complaints;Username=postgres;Password=postgres");

        return new ComplaintDbContext(optionsBuilder.Options);
    }
}
