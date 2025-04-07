using Microsoft.EntityFrameworkCore;
using TransactionsService.Core.Models.Enums;
using TransactionsService.Core.Models.Entities;

namespace TransactionsService.Data.DatabaseContexts
{
    public class TransactionsDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Transaction>()
                .Property(entity => entity.Currency)
                .HasConversion(
                    value => value.ToString(),
                    value => Enum.Parse<Currency>(value));

            modelBuilder
                .Entity<Transaction>()
                .Property(entity => entity.Status)
                .HasConversion(
                    value => value.ToString(),
                    value => Enum.Parse<TransactionStatus>(value));
        }
    }
}
