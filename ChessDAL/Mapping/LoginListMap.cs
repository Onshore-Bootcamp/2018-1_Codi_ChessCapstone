using CapstoneDAO.Models;
using System;
using System.Data;

namespace CapstoneDAO.Mapping
{
    public class LoginListMap
    {
        //Method for assigning data from database values to c# values
        public static LoginDO RowToItem(DataRow iSource)
        {
            //Establishing a object using model LoginDO
            LoginDO to = new LoginDO();
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
            if (iSource["Password"] != DBNull.Value)
            {
                //pulling "Value" from SQL, casting it or converting it as necessary, and assigning it to it's c# equivalent
                to.Password = iSource["Password"].ToString();
            }
            //Return all data to previous method
            return to;
        }
    }
}