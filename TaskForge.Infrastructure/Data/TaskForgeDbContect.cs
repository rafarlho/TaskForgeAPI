using Microsoft.EntityFrameworkCore;
using TaskForge.Domain.Entities;

namespace TaskForge.Infrastructure.Data;
public class TaskForgeDbContext : DbContext
{
    public TaskForgeDbContext(DbContextOptions<TaskForgeDbContext> options)
    : base(options){}

    public DbSet<Organization> Organizaions => Set<Organization>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.CreatedAt)
                .IsRequired();
            
            entity.Property(e => e.UpdatedAt)
                .IsRequired();
            
            entity.HasIndex(e => e.Name);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added && entry.Entity is Organization addedOrg)
            {
                addedOrg.CreatedAt = DateTime.UtcNow;
                addedOrg.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified && entry.Entity is Organization modifiedOrg)
            {
                modifiedOrg.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}