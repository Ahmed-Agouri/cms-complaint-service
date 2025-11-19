using ComplaintService.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintService.Infrastructure.Data;

public class ComplaintDbContext: DbContext
{
    
    public ComplaintDbContext(DbContextOptions<ComplaintDbContext> options)
        : base(options)
    {
    }

    public DbSet<Complaint> Complaints { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(2000);
        });
    }
}
