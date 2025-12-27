using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ComplaintService.Infrastructure.Data;

public class ComplaintDbContextFactory : IDesignTimeDbContextFactory<ComplaintDbContext>
{
    public ComplaintDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ComplaintDbContext>();

        // For EF CLI usage (local development)
        optionsBuilder.UseSqlite("Data Source=complaints.db");

        return new ComplaintDbContext(optionsBuilder.Options);
    }
}