using System.ComponentModel.DataAnnotations.Schema;

namespace ZeherCricket.Data
{
    public class UserMatchSelection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string UserName { get; set; }

        public string TeamName { get; set; }

        public int matchId { get; set; }

        public DateTime Date { get; set; }
    }
}
