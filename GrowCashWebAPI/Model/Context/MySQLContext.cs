using Microsoft.EntityFrameworkCore;

namespace GrowCashWebAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {

        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options){}

        public DbSet<UserModel> Users { get; set;}

        public DbSet<AccountModel> Accounts { get; set;}
        public DbSet<RevenueModel> Revenues { get; set;}
        public DbSet<ExpenseModel> Expenses { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .ToTable("Users")
                .HasKey(a => a.Id);

            modelBuilder.Entity<AccountModel>()
                .ToTable("Accounts")
                .HasKey(a => a.Id);
                
            modelBuilder.Entity<AccountModel>()
                .HasOne(r => r.User)
                .WithMany(c => c.Account)
                .HasForeignKey(r => r.Id_User);

            modelBuilder.Entity<RevenueModel>()
                .HasOne(r => r.Account)
                .WithMany(c => c.Revenues)
                .HasForeignKey(r => r.Id_Account);

            modelBuilder.Entity<ExpenseModel>()
                .HasOne(e => e.Account)
                .WithMany(a => a.Expenses)
                .HasForeignKey(e => e.Id_Account);


            //base.OnModelCreating(modelBuilder);
        }
    }
}