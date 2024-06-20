using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZeherCricket.Data.Config
{
    public class MatchInfoTableConfig : IEntityTypeConfiguration<MatchInfo>
    {
        public void Configure(EntityTypeBuilder<MatchInfo> builder)
        {
            builder.ToTable("MatchInfoTable");
            builder.HasKey(x => x.matchId);

            builder.Property(x => x.matchNumber).IsRequired();
            builder.Property(x => x.FirstTeam).IsRequired();
            builder.Property(x => x.SecondTeam).IsRequired();
            builder.Property(x => x.Winner).IsRequired();
        }
    }
}

