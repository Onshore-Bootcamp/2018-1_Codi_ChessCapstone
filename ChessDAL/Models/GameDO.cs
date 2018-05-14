using System;
using System.ComponentModel.DataAnnotations;

namespace CapstoneDAO.Models
{
    public class GameDO
    {
        public long GameId { get; set; }

        [Required]
        public long White { get; set; }

        public string WhiteName { get; set; }

        public int WhiteRating { get; set; }

        [Required]
        public long Black { get; set; }

        public string BlackName { get; set; }

        public int BlackRating { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime? DatePlayed { get; set; }

        [Required]
        public string Winner { get; set; }

        public int FavoritePlayerId { get; set; }
    }
}