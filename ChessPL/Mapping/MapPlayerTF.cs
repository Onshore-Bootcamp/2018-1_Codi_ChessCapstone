using CapstoneDAO.Models;
using CapstonePL.Models;

namespace CapstonePL.Mapping
{
    public class MapPlayerTF
    {
        //Method for mapping from DO to PO
        public static PlayerPO PlayerDOtoPO(PlayerDO from)
        {
            //Declaring a new PO object using the PlayerPO model
            PlayerPO to = new PlayerPO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.PlayerId = from.PlayerId;
            to.Name = from.Name;
            to.PeakRating = from.PeakRating;
            to.BirthDate = from.BirthDate;
            to.Dead = from.Dead;
            to.CountryOfOrigin = from.CountryOfOrigin;
            to.CountryRepresented = from.CountryRepresented;
            //sends the data back to the method that called it
            return to;
        }
        //Method for mapping from PO to DO
        public static PlayerDO PlayerPOtoDO(PlayerPO from)
        {
            //Declaring a new DO object using the PlayerDO model
            PlayerDO to = new PlayerDO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.PlayerId = from.PlayerId;
            to.Name = from.Name;
            to.PeakRating = from.PeakRating;
            to.BirthDate = from.BirthDate;
            to.Dead = from.Dead;
            to.CountryOfOrigin = from.CountryOfOrigin;
            to.CountryRepresented = from.CountryRepresented;
            //sends the data back to the method that called it
            return to;
        }

    }
}