using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.TodoDbContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(n => n.Name)
            .HasMaxLength(50);

        modelBuilder.Entity<User>()
            .HasIndex(n => n.Email)
            .IsUnique();

        modelBuilder.Entity<Note>()
            .HasOne(u => u.User)
            .WithMany(n => n.Notes)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}