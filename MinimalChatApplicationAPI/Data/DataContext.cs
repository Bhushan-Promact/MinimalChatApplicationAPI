using Microsoft.EntityFrameworkCore;
using MinimalChatApplicationAPI.Model;
using System.Text.RegularExpressions;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Message>()
                .HasOne(x => x.User1)
                .WithMany(a => a.SentMessage)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message>()
                .HasOne(x => x.User2)
                .WithMany(a => a.ReceivedMessage)
                .HasForeignKey(x => x.ReceiverId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
