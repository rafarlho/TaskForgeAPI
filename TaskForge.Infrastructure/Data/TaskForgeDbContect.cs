using Microsoft.EntityFrameworkCore;
using TaskForge.Domain.Entities;
using Task = TaskForge.Domain.Entities.Task;

namespace TaskForge.Infrastructure.Data;
public class TaskForgeDbContext : DbContext
{
    public TaskForgeDbContext(DbContextOptions<TaskForgeDbContext> options)
    : base(options){}

    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<TaskGroup> TaskGroups => Set<TaskGroup>();
    public DbSet<Task> Tasks => Set<Task>();

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

            entity.Property(e => e.Version)
                .IsRowVersion();

            entity.HasIndex(e => e.Name);

            entity.HasMany(e => e.TaskGroups)
                .WithOne(tg => tg.Organization)
                .HasForeignKey(tg => tg.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TaskGroup>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            entity.Property(e => e.Version)
                .IsRowVersion();

            entity.Property(t => t.Status)
                .HasConversion<string>();

            entity.Property(e => e.Description)
                .HasMaxLength(200);

            entity.HasIndex(e => e.Name);

            entity.HasMany(e => e.Tasks)
                .WithOne(t => t.TaskGroup)
                .HasForeignKey(e => e.TaskGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasMaxLength(200);

            entity.Property(e => e.Assignee)
                .HasMaxLength(200);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            entity.Property(e => e.Version)
                .IsRowVersion();

            entity.Property(t => t.Status)
                .HasConversion<string>();


            entity.HasIndex(e => e.Name);

            entity.HasOne(e => e.TaskGroup)
                .WithMany(o => o.Tasks)
                .HasForeignKey(e => e.TaskGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateBaseEntityFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateBaseEntityFields();
        return base.SaveChanges();
    }

    private void UpdateBaseEntityFields()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Id = Guid.NewGuid();
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
                entry.Property(e => e.CreatedAt).IsModified = false;
            }
        }
    }
}