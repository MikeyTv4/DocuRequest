using DocuRequest.Models;
using Microsoft.EntityFrameworkCore;

namespace DocuRequest.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<DocumentRequest> DocumentRequests => Set<DocumentRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocumentRequest>()
            .HasIndex(r => r.ReferenceNo)
            .IsUnique();

        // Store enums as readable text in the database.
        modelBuilder.Entity<DocumentRequest>()
            .Property(r => r.Status).HasConversion<string>();
        modelBuilder.Entity<DocumentRequest>()
            .Property(r => r.DocumentType).HasConversion<string>();
    }
}
