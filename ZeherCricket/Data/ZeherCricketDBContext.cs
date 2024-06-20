using Microsoft.EntityFrameworkCore;
using ZeherCricket.Data.Config;

namespace ZeherCricket.Data
{
    public class ZeherCricketDBContext : DbContext
    {
        public ZeherCricketDBContext(DbContextOptions<ZeherCricketDBContext> options) : base(options)
        {
            
        }

        public DbSet<UserInfo> UsersTable { get; set; }

        public DbSet<MatchInfo> MatchInfoTable { get; set; }

        public DbSet<UserMatchSelection> UserMatchSelectionTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersTableConfig());
            modelBuilder.ApplyConfiguration(new MatchInfoTableConfig());
            modelBuilder.ApplyConfiguration(new UserMatchSelectionTableConfig());
        }
    }
}
