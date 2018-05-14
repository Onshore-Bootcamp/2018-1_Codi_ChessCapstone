using CapstoneDAO.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapstoneDAO.Mapping
{
    public class GameListMap
    {
        //Method for converting datatable data into object aspects data
        public static List<GameDO> DataTableToList(DataTable source)
        {
            //Establishing a list using model GameDO
            List<GameDO> allGames = new List<GameDO>();
            //if statement checking for null and row count greater than zero
            if (source != null && source.Rows.Count >= 0)
            {
                //loop that assigns data to our list row by row, and item by item
                foreach (DataRow row in source.Rows)
                {
                    //assigning data to list
                    allGames.Add(RowToItem(row));
                }
            }
            //send all data back to PL
            return allGames;
        }
        //Method for assigning data from database values to c# values
        private static GameDO RowToItem(DataRow iSource)
        {
            //establishing new object
            GameDO to = new GameDO();
            //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
            to.GameId = (long)iSource["GameId"];
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["WhitePlayer"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.WhiteName = iSource["WhitePlayer"].ToString();
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["WhiteRating"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.WhiteRating = (int)iSource["WhiteRating"];
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["BlackPlayer"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.BlackName = iSource["BlackPlayer"].ToString();
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["BlackRating"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.BlackRating = (int)iSource["BlackRating"];
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["Location"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.Location = iSource["Location"].ToString();
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["DatePlayed"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.DatePlayed = (DateTime)iSource["DatePlayed"];
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["Winner"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.Winner = iSource["Winner"].ToString();
            }
            //Return all data to previous method
            return to;
        }
    }
}