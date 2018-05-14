using ChessPL.Models;
using ChessDAL.Models;

namespace ChessPL.Mapping
{
    public class MapWinnersTF
    {
        //Method for mapping from DO to PO
        public static WinsPO WinsDOtoPO(WinsDO from)
        {
            //Declaring a new PO object using the WinsPO model
            WinsPO to = new WinsPO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.Winner = from.Winner;
            to.Wins = from.Wins;
            //sends the data back to the method that called it
            return to;
        }
        //Method for mapping from PO to DO
        public static WinsDO WinsPOtoDO(WinsPO from)
        {
            //Declaring a new DO object using the WinsDO model
            WinsDO to = new WinsDO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.Winner = from.Winner;
            to.Wins = from.Wins;
            //sends the data back to the method that called it
            return to;
        }
    }
}
