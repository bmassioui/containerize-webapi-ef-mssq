using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    /// <summary>
    /// TodoItem DbContext
    /// </summary>
    public class TodoItemDbContext : DbContext
    {
        public TodoItemDbContext(DbContextOptions<TodoItemDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}