using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZeherCricket.Data.Config
{
    public class UsersTableConfig : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable("UsersTable");
            builder.HasKey(x => x.UserName);
        }
    }
}
