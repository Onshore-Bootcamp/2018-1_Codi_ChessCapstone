using CapstoneDAO;
using CapstoneDAO.Models;
using CapstonePL.ErrorTextPL;
using CapstonePL.Mapping;
using CapstonePL.Models;
using ChessPL.Controllers.ActionFilters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using ChessDAL.Models;
using ChessPL.Mapping;
using ChessPL.Models;

namespace CapstonePL.Controllers
{
    public class PlayerController : Controller
    {
        //Constructor for assigning fields for connections
        public PlayerController()
        {
            //setting variable equal to the SqlConnection in appConfig
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            //setting fields equal to new variant connections
            _dataAccess = new PlayerDAO(connection);
        }

        //Declaration of fields for DAL access
        private PlayerDAO _dataAccess;

        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(1, 2, 3)]
        //Method of getting specific information for a player via DAL access call
        public ActionResult DetailPlayer(int playerId)
        {
            //declaring list using model GamePO
            List<PlayerPO> mappedPlayer = new List<PlayerPO>();
            //Beginning of processes
            try
            {
                //declaring list using Model PlayerDO in order to retrieve database information
                List<PlayerDO> player = _dataAccess.PlayerDetails(playerId);
                //loop to get all objects assigned appropriately
                foreach (PlayerDO dataObject in player)
                {
                    //assign our PO list all of the values that were in the DO list via mapper
                    mappedPlayer.Add(MapPlayerTF.PlayerDOtoPO(dataObject));
                }
            }
            //catch to record any exceptions that crop up
            catch (Exception ex)
            {
                //call to method to record necessary information
                ErrorFile.ErrorHandlerPL(ex);
            }
            //finally to tie up any loose ends
            finally
            { }
            //Sends the data in the list to the view to be seen by the user.
            return View(mappedPlayer);
        }

        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(1, 2, 3)]
        //Method of getting general information for a player via DAL access call
        public ActionResult Index()
        {
            //declaring list using model GamePO
            List<PlayerPO> mappedPlayers = new List<PlayerPO>();
            //Beginning of processes
            try
            {
                //declaring list using Model PlayerDO in order to retrieve database information
                List<PlayerDO> allPlayers = _dataAccess.PlayerReadAll();
                //loop to get all objects assigned appropriately
                foreach (PlayerDO dataObject in allPlayers)
                {
                    //assign our PO list all of the values that were in the DO list via mapper
                    mappedPlayers.Add(MapPlayerTF.PlayerDOtoPO(dataObject));
                }
            }
            //catch to record any exceptions that crop up
            catch (Exception ex)
            {
                //call to method to record necessary information
                ErrorFile.ErrorHandlerPL(ex); 
            }
            //finally to tie up any loose ends
            finally
            { }
            //Sends the data in the list to the view to be seen by the user.
            return View(mappedPlayers);
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(2, 3)]
        //Method of getting all information from a User to add a player to the database
        public ActionResult AddPlayer()
        {
            //Beginning of processes
            try
            { }
            //catch to record any exceptions that crop up
            catch (Exception ex)
            {
                //call to method to record necessary information
                ErrorFile.ErrorHandlerPL(ex);
            }
            //finally to tie up any loose ends
            finally
            { }
            //Sends the user to the view
            return View();
        }

        //Determines whether information is being taken in, or sent out
        [HttpPost]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(2, 3)]
        //Method to pass all the information gathered from the user and add a player to the database
        public ActionResult AddPlayer(PlayerPO form)
        {
            //Declaring an action result variable, but assigning it to null to reassign later
            ActionResult result = null;
            //Checks to see if the UI form is filled out correctly
            if (ModelState.IsValid)
            {
                //Beginning of processes
                try
                {
                    //passing the UI form information to run through our mapper, and assigning it to a DO object
                    PlayerDO dataObject = MapPlayerTF.PlayerPOtoDO(form);
                    //calling on a DAL access field to allow us to use a specific method within, while passing in the DO object
                    _dataAccess.PlayerCreate(dataObject);
                }
                //catch to record any exceptions that crop up
                catch (Exception ex)
                {
                    //call to method to record necessary information
                    ErrorFile.ErrorHandlerPL(ex);
                }
                //finally to tie up any loose ends
                finally
                { }
                //assigns a page redirection to our variable
                result = RedirectToAction("Index", "Player");
            }
            //section of code that runs if the form is not valid
            else
            {
                //assigns a page redirection to our variable
                result = View(form);
            }
            //runs the portion of code that is necessary for the situation
            return result;
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(2, 3)]
        //Method to gather information from a user to update a player
        public ActionResult UpdatePlayer(int playerId)
        {
            //declaring object using model PlayerPO
            PlayerPO playerToUpdate = new PlayerPO();
            //Beginning of processes
            try
            {
                //declare List using Model PlayerDO, and use it to store all information on the game recovered by using a DAL access call
                PlayerDO item = _dataAccess.PlayerReadByID(playerId);
                //assign all data to object using a mapper
                playerToUpdate = MapPlayerTF.PlayerDOtoPO(item);
            }
            //catch to record any exceptions that crop up
            catch (Exception ex)
            {
                //call to method to record necessary information
                ErrorFile.ErrorHandlerPL(ex);
            }
            //finally to tie up any loose ends
            finally
            { }
            //Sends the data in the list to the view to be seen by the user.
            return View(playerToUpdate);
        }

        //Determines whether information is being taken in, or sent out
        [HttpPost]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(2, 3)]
        //Method to pass in all information gathered from a user and update a player's information in the database
        public ActionResult UpdatePlayer(PlayerPO form)
        {
            //Declaring an action result variable, but assigning it to null to reassign later
            ActionResult result = null;
            //Checks to see if the UI form is filled out correctly
            if (ModelState.IsValid)
            {
                //Beginning of processes
                try
                {
                    //passing the UI form information to run through our mapper, and assigning it to a DO object
                    PlayerDO dataObject = MapPlayerTF.PlayerPOtoDO(form);
                    //calling on a DAL access field to allow us to use a specific method within, while passing in the DO object
                    _dataAccess.PlayerUpdate(dataObject);
                }
                //catch to record any exceptions that crop up
                catch (Exception ex)
                {
                    //call to method to record necessary information
                    ErrorFile.ErrorHandlerPL(ex);
                }
                //finally to tie up any loose ends
                finally
                { }
                //assigns a page redirection to our variable
                result = RedirectToAction("Index", "Player");
            }
            //section of code that runs if the form is not valid
            else
            {
                //assigns a page redirection to our variable
                result = View(form);
            }
            //runs the portion of code that is necessary for the situation
            return result;
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(3)]
        //Method to delete a player from the database
        public ActionResult DeletePlayer(int playerId)
        {
            //Beginning of processes
            try
            {
                //passes playerId in to the player delete method to delete the player assigned to the id
                _dataAccess.PlayerDelete(playerId);
            }
            //catch to record any exceptions that crop up
            catch (Exception ex)
            {
                //call to method to record necessary information
                ErrorFile.ErrorHandlerPL(ex);
            }
            //finally to tie up any loose ends
            finally
            { }
            //sends a page redirection to our user
            return RedirectToAction("Index", "Player");
        }

        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(1, 2, 3)]
        //Method to call data back from the database consisting of the player name, and their win total
        public ActionResult PlayersByWins()
        {
            //declaring list using model WinsPO
            List<WinsPO> players = new List<WinsPO>();
            //Beginning of processes
            try
            {
                //declaring list using Model Wins in order to retrieve database information
                List<WinsDO> player = _dataAccess.OrderByWinCount();
                //loop to get all objects assigned appropriately
                foreach (WinsDO dataObject in player)
                {
                    //assign our PO list all of the values that were in the DO list via mapper
                    players.Add(MapWinnersTF.WinsDOtoPO(dataObject));
                }
            }
            //catch to record any exceptions that crop up
            catch (Exception ex)
            {
                //call to method to record necessary information
                ErrorFile.ErrorHandlerPL(ex);
            }
            //finally to tie up any loose ends
            finally
            { }
            //Sends the User to the View page
            return View(players);
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(1, 2, 3)]
        //Method to search the database for a player by the name of the player
        public ActionResult ReadPlayerByName(string winner, int wins)
        {
            //declaring list using model GamePO
            List<PlayerPO> mappedPlayer = new List<PlayerPO>();
            //Beginning of processes
            try
            {
                //declaring list using Model PlayerDO in order to retrieve database information
                List<PlayerDO> player = _dataAccess.PlayerReadByName(winner);
                //loop to get all objects assigned appropriately
                foreach (PlayerDO dataObject in player)
                {
                    //assign our PO list all of the values that were in the DO list via mapper
                    mappedPlayer.Add(MapPlayerTF.PlayerDOtoPO(dataObject));
                }
            }
            //catch to record any exceptions that crop up
            catch (Exception ex)
            {
                //call to method to record necessary information
                ErrorFile.ErrorHandlerPL(ex);
            }
            //finally to tie up any loose ends
            finally
            { }
            //Sends the data in the list to the view to be seen by the user.
            return View(mappedPlayer);
        }
    }
}