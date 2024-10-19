using DynamicFormBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicFormBuilder.Infrastructure.Context
{
    public class DynamicFormBuilderDbContext : DbContext
    {
        public DynamicFormBuilderDbContext(DbContextOptions<DynamicFormBuilderDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Control> Controls { get; set; }
        public DbSet<Form> Forms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id); // Primary key
                entity.Property(u => u.Id)
                      .ValueGeneratedOnAdd(); // Auto-increment Id

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(255); // Restricting email length for indexing
                entity.Property(u => u.Password)
                    .IsRequired();

                // Indexes for performance optimization
                entity.HasIndex(u => u.Email)
                    .IsUnique(); // Enforce unique constraint on Email for quick lookup

                entity.Property(u => u.CreatedBy)
                    .HasMaxLength(100);

                entity.Property(u => u.CreatedDate)
                    .IsRequired();

                entity.HasMany(u => u.Forms)
                    .WithOne(f => f.User)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete for related Forms
            });

            // Form entity configuration
            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasKey(f => f.Id); // Primary key
                entity.Property(f => f.Id)
                      .ValueGeneratedOnAdd(); // Auto-increment Id

                entity.Property(f => f.Key)
                    .IsRequired()
                    .HasMaxLength(100); // Key for identifying forms

                entity.Property(f => f.Name)
                    .IsRequired()
                    .HasMaxLength(255); // Form name

                entity.Property(f => f.CreatedBy)
                    .HasMaxLength(100);

                entity.Property(f => f.CreatedDate)
                    .IsRequired(); // Form creation date

                // Nullable ModifiedDate and ModifiedBy
                entity.Property(f => f.ModifiedDate)
                    .IsRequired(false); // Nullable field for modification date

                entity.Property(f => f.ModifiedBy)
                    .HasMaxLength(100)
                    .IsRequired(false); // Nullable field for modified by

                // Index for quick lookup on Key
                entity.HasIndex(f => f.Key);

                entity.HasMany(f => f.Controls)
                    .WithOne(c => c.Form)
                    .HasForeignKey(c => c.FormId)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete for related Controls
            });

            // Control entity configuration
            modelBuilder.Entity<Control>(entity =>
            {
                entity.HasKey(c => c.Id); // Primary key
                entity.Property(c => c.Id)
                      .ValueGeneratedOnAdd(); // Auto-increment Id

                entity.Property(c => c.Key)
                    .IsRequired()
                    .HasMaxLength(100); // Control Key

                entity.Property(c => c.Label)
                    .IsRequired()
                    .HasMaxLength(255); // Control Label

                entity.Property(c => c.Type)
                    .IsRequired()
                    .HasMaxLength(50); // Control Type (e.g., text, checkbox)

                entity.Property(c => c.Value)
                    .HasMaxLength(500); // Optional value length limit

                entity.Property(c => c.OptionsJson)
                    .HasColumnType("nvarchar(max)"); // Control options in JSON format

                entity.Property(c => c.CreationDate)
                    .IsRequired(); // Control creation date

                entity.Property(c => c.ModifiedDate)
                    .IsRequired(false); // Nullable modified date for Control

                // Indexes for Control optimization
                entity.HasIndex(c => c.Key);
                entity.HasIndex(c => new { c.FormId, c.Key })
                    .IsUnique(); // Ensure unique control per form by key
            });
        }
    }
}
