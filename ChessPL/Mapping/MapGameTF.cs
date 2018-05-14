using CapstoneDAO.Models;
using CapstonePL.Models;

namespace CapstonePL.Mapping
{
    public class MapGameTF
    {
        //Method for mapping from DO to PO
        public static GamePO GameDOtoPO(GameDO from)
        {
            //Declaring a new PO object using the GamePO model
            GamePO to = new GamePO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.GameId = from.GameId;
            to.White = from.White;
            to.WhiteName = from.WhiteName;
            to.WhiteRating = from.WhiteRating;
            to.Black = from.Black;
            to.BlackName = from.BlackName;
            to.BlackRating = from.BlackRating;
            to.Location = from.Location;
            to.DatePlayed = from.DatePlayed;
            to.Winner = from.Winner;
            //sends the data back to the method that called it
            return to;
        }
        //Method for mapping from DO to PO
        public static GameDO GamePOtoDO(GamePO from)
        {
            //Declaring a new DO object using the GameDO model
            GameDO to = new GameDO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.GameId = from.GameId;
            to.White = from.White;
            to.WhiteName = from.WhiteName;
            to.WhiteRating = from.WhiteRating;
            to.Black = from.Black;
            to.BlackName = from.BlackName;
            to.BlackRating = from.BlackRating;
            to.Location = from.Location;
            to.DatePlayed = from.DatePlayed;
            to.Winner = from.Winner;
            //sends the data back to the method that called it
            return to;
        }
    }
}