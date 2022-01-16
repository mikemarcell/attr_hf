using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApp.Db.Model;

namespace TodoApp.Db
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserPicture> UserPictures { get; set; }

        private string ConnectionString { get; }

        public TodoContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();

            user.HasIndex(u => u.Email)
                .IsUnique();

            user.HasMany<TodoItem>(u => u.TodoItems)
                .WithOne(t => t.Owner)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            user.HasOne(u => u.UserPicture)
                .WithOne(u => u.User)
                .HasForeignKey<UserPicture>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}