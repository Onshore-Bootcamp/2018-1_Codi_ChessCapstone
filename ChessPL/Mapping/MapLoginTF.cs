using CapstoneDAO.Models;
using CapstonePL.Models;

namespace CapstonePL.Mapping
{
    public class MapLoginTF
    {
        //Method for mapping from DO to PO
        public static LoginPO LoginDOtoPO(LoginDO from)
        {
            //Declaring a new PO object using the WinsPO model
            LoginPO to = new LoginPO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.UserId = from.UserId;
            to.UserRoleId = from.UserRoleId;
            to.Username = from.Username;
            to.Password = from.Password;
            //sends the data back to the method that called it
            return to;
        }
        //Method for mapping from PO to DO
        public static LoginDO LoginPOtoDO(LoginPO from)
        {
            //Declaring a new DO object using the WinsDO model
            LoginDO to = new LoginDO();
            //Mapping all relevant information. from is the pass in, to is the result.
            to.Username = from.Username;
            to.Password = from.Password;
            //sends the data back to the method that called it
            return to;
        }
    }
}