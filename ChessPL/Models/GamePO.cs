using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CapstonePL.Models
{
    public class GamePO
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

        public List<SelectListItem> PlayersDropDown { get; set; }

        public int SelectedPlayerWhite { get; set; }

        public int SelectedPlayerBlack { get; set; }
    }
}
