using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoDataAPI.Models;

namespace TodoDataAPI
{
    public class TodoContext : IdentityDbContext
    {
        public TodoContext
            (DbContextOptions<TodoContext> options)
        : base(options)
        { }
        public TodoContext():base()
        {
            
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
