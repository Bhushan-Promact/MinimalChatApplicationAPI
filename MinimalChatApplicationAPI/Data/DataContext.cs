using Microsoft.EntityFrameworkCore;
using MinimalChatApplicationAPI.Model;

namespace MinimalChatApplicationAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Message> Messages { get; set; } = default!;
    }
}
