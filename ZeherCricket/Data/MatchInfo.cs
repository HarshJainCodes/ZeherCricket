using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ZeherCricket.Data
{
    public class MatchInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int matchId { get; set; }

        public int matchNumber { get; set; }

        public string FirstTeam { get; set; }

        public string SecondTeam { get; set; }

        public DateTime MatchDate { get; set; }

        public bool IsSundayFirstMatch { get; set; } = false;

        [AllowNull]
        public string? Winner { get; set; }
    }
}
