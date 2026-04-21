using KtorresLibreriaDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace KtorresLibreriaDigital.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Books)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(b => b.Author)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Reviews)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .Property(r => r.Comment)
            .HasMaxLength(1000);

        modelBuilder.Entity<Review>()
            .ToTable(t => t.HasCheckConstraint("CK_Review_Rating", "Rating >= 1 AND Rating <= 5"));
    }
}
