using ChessDAL.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ChessDAL.Mapping
{
    class WinMap
    {
        //Method for converting datatable data into object aspects data
        public static List<WinsDO> DataTableToList(DataTable source)
        {
            //Establishing a list using model WinsDO
            List<WinsDO> allGames = new List<WinsDO>();
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
        public static WinsDO RowToItem(DataRow iSource)
        {
            //Establishing a object using model WinsDO
            WinsDO to = new WinsDO();
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["Winner"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.Winner = iSource["Winner"].ToString();
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["NumberOfWins"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.Wins = (int)iSource["NumberOfWins"];
            }
            //Return all data to previous method
            return to;
        }
    }
}
