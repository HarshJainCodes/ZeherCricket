using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZeherCricket.Data.Config
{
    public class UserMatchSelectionTableConfig : IEntityTypeConfiguration<UserMatchSelection>
    {
        public void Configure(EntityTypeBuilder<UserMatchSelection> builder)
        {
            builder.ToTable("UserMatchSelectionTable");
            builder.HasKey(x => x.id);

            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.TeamName).IsRequired();
            builder.Property(x => x.Date).IsRequired();
        }
    }
}
