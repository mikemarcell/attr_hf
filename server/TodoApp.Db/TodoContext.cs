using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection;
using TodoApp.Db.Model;

namespace TodoApp.Db
{
  public class TodoContext : DbContext
  {
    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<User> Users { get; set; }

    private string ConnectionString { get; }

    public TodoContext(IConfiguration configuration)
    {
      ConnectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
          .HasMany<TodoItem>(u => u.TodoItems)
          .WithOne(t => t.Owner)
          .HasForeignKey(t => t.OwnerId)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}