using CapstoneDAO.Models;
using CapstonePL.Models;
using System.Collections.Generic;

namespace CapstonePL.Mapping
{
    public class MapUserTF
    {
        //Method for mapping from DO to PO
        public static UserPO UserDOtoPO(UserDO from)
        {
            //Declaring a new PO object using the UserPO model
            UserPO to = new UserPO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.UserId = from.UserId;
            to.UserRoleId = from.UserRoleId;
            to.Username = from.Username;
            to.Password = from.Password;
            to.Role = from.Role;
            to.LastName = from.LastName;
            to.FirstName = from.FirstName;
            to.Email = from.Email;
            //sends the data back to the method that called it
            return to;
        }
        //Method for mapping from PO to DO
        public static UserDO UserPOtoDO(UserPO from)
        {
            //Declaring a new DO object using the UserDO model
            UserDO to = new UserDO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.UserId = from.UserId;
            to.UserRoleId = from.UserRoleId;
            to.Username = from.Username;
            to.Password = from.Password;
            to.Role = from.Role;
            to.LastName = from.LastName;
            to.FirstName = from.FirstName;
            to.Email = from.Email;
            //sends the data back to the method that called it
            return to;
        }
    }
}