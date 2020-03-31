using Microsoft.EntityFrameworkCore;
using TodoDataAPI.Models;

namespace TodoDataAPI
{
    public class TodoContext : DbContext
    {
        public TodoContext
            (DbContextOptions<TodoContext> options)
        : base(options)
        { }
        public TodoContext():base()
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
