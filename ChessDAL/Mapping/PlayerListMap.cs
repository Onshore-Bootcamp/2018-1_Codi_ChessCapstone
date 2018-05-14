using CapstoneDAO.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapstoneDAO.Mapping
{
    public class PlayerListMap
    {
        //Method for converting datatable data into object aspects data
        public static List<PlayerDO> DataTableToList(DataTable source)
        {
            //Establishing a list using model PlayerDO
            List<PlayerDO> players = new List<PlayerDO>();
            //if statement checking for null and row count greater than zero
            if (source != null && source.Rows.Count > 0)
            {
                //loop that assigns data to our list row by row, and item by item
                foreach (DataRow row in source.Rows)
                {
                    //assigning data to list
                    players.Add(RowToItem(row));
                }
            }
            //send all data back to PL
            return players;
        }
        //Method for assigning data from database values to c# values
        public static PlayerDO RowToItem(DataRow iSource)
        {
            //Establishing a object using model PlayerDO
            PlayerDO to = new PlayerDO();
            //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
            to.PlayerId = (long)iSource["PlayerId"];
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["Name"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.Name = iSource["Name"].ToString();
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["PeakRating"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.PeakRating = (int)iSource["PeakRating"];
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["BirthDate"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.BirthDate = (DateTime)iSource["BirthDate"];
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["Dead"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.Dead = (DateTime)iSource["Dead"];
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["CountryOfOrigin"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.CountryOfOrigin = iSource["CountryOfOrigin"].ToString();
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["CountryRepresented"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.CountryRepresented = iSource["CountryRepresented"].ToString();
            }
            //Return all data to previous method
            return to;
        }
    }
}