using CapstoneDAO.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapstoneDAO.Mapping
{
    public class UserListMap
    {
        //Method for converting datatable data into object aspects data
        public static List<UserDO> DataTableToList(DataTable source)
        {
            //Establishing a list using model UserDO
            List<UserDO> allUsers = new List<UserDO>();
            //if statement checking for null and row count greater than zero
            if (source != null && source.Rows.Count > 0)
            {
                //loop that assigns data to our list row by row, and item by item
                foreach (DataRow row in source.Rows)
                {
                    //assigning data to list
                    allUsers.Add(RowToItem(row));
                }
            }
            //send all data back to PL
            return allUsers;
        }
        //Method for assigning data from database values to c# values
        private static UserDO RowToItem(DataRow iSource)
        {
            //Establishing a object using model UserDO
            UserDO to = new UserDO();
            //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
            to.UserId = (long)iSource["UserId"];
            //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
            to.UserRoleId = (int)iSource["UserRoleId"];
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["Username"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.Username = iSource["Username"].ToString();
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["LastName"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.FirstName = iSource["LastName"].ToString();
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["FirstName"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.LastName = iSource["FirstName"].ToString();
            }
            //Checking if SQL value is null or not, and if not, proceeds into the if
            if (iSource["Email"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.Email = iSource["Email"].ToString();
            }
            //Return all data to previous method
            return to;
        }
    }
}