using System;

namespace CapstonePL.Models
{
    public class PlayerPO
    {
        public long PlayerId { get; set; }

        public string Name { get; set; }

        public int PeakRating { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? Dead { get; set; }

        public string CountryOfOrigin { get; set; }

        public string CountryRepresented { get; set; }
    }
}