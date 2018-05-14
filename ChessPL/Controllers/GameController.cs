using CapstoneDAO;
using CapstoneDAO.Models;
using CapstonePL.ErrorTextPL;
using CapstonePL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using CapstonePL.Mapping;
using System.Web.Mvc;
using ChessPL.Controllers.ActionFilters;
using CapstoneDAO.Mapping;

namespace CapstonePL.Controllers
{
    public class GameController : Controller
    {
        //Constructor for assigning fields for connections
        public GameController()
        {
            //setting variable equal to the SqlConnection in appConfig
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            //setting fields equal to new variant connections
            _dataAccess = new GameDAO(connection);
            _playerAccess = new PlayerDAO(connection);
        }

        //Declaration of fields for DAL access
        private GameDAO _dataAccess;
        private PlayerDAO _playerAccess;

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(1, 2, 3)]
        //Method of getting the detailed information of a game from the database and sending it up to the user
        public ActionResult DetailGame(int gameId)
        {
            //declaring list using model GamePO
            List<GamePO> mappedGame = new List<GamePO>();
            //Beginning of processes
            try
            {
                //declaring list using Model GameDO in order to retrieve database information
                List<GameDO> game = _dataAccess.GameDetails(gameId);
                //loop to get all objects assigned appropriately
                foreach (GameDO dataObject in game)
                {
                    //assign our PO list all of the values that were in the DO list via mapper
                    mappedGame.Add(MapGameTF.GameDOtoPO(dataObject));
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
            return View(mappedGame);
        }

        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(1, 2, 3)]
        //Method of getting the general information of a game from the database, and sending it up to the user
        public ActionResult Index()
        {
            //declaring list using model GamePO
            List<GamePO> mappedGame = new List<GamePO>();
            //Beginning of processes
            try
            {
                //declaring list using Model GameDO in order to retrieve database information
                List<GameDO> allGames = _dataAccess.GameReadAll();
                //loop to get all objects assigned appropriately
                foreach (GameDO dataObject in allGames)
                {
                    //assign our PO list all of the values that were in the DO list via mapper
                    mappedGame.Add(MapGameTF.GameDOtoPO(dataObject));
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
            return View(mappedGame);
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(2, 3)]
        //Method for taking in the necessary data to add a new game to the database
        public ActionResult AddGame()
        {
            //declaring object using model GamePO
            GamePO game = new GamePO();
            //Beginning of processes
            try
            {
                //declare List using Model PlayerDO, and use it to store all information on all players recovered by using a DAL access call
                List<PlayerDO> players = _playerAccess.PlayerReadAll();
                //Declare list to hold player names
                List<SelectListItem> options = new List<SelectListItem>();
                //loop to get all objects assigned appropriately
                foreach (PlayerDO dataObject in players)
                {
                    //Declare new object to temporarily hold data
                    SelectListItem option = new SelectListItem();
                    //make list aspect equal dataObject variable
                    option.Text = dataObject.Name;
                    //make list aspect equal dataObject variable
                    option.Value = dataObject.PlayerId.ToString();
                    //take object "option", and add it to list "options"
                    options.Add(option);
                }
                //set object value of PlayersDropDown equal to values of "options"
                game.PlayersDropDown = options;
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
            return View(game);
        }

        //Determines whether information is being taken in, or sent out
        [HttpPost]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(2, 3)]
        //method for passing in the User information from the PL to the DL, and then to the database
        public ActionResult AddGame(GamePO form)
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
                    GameDO dataObject = MapGameTF.GamePOtoDO(form);
                    //calling on a DAL access field to allow us to use a specific method within, while passing in the DO object
                    _dataAccess.GameCreate(dataObject);

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
                result = RedirectToAction("Index", "Game");
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
        //Takes in the information that a user wants to update about a certain game
        public ActionResult UpdateGame(int gameId)
        {
            //declaring object using model GamePO
            GamePO gameToUpdate = new GamePO();
            //Beginning of processes
            try
            {
                //declare List using Model GameDO, and use it to store all information on the game recovered by using a DAL access call
                GameDO item = _dataAccess.GameReadByID(gameId);
                //assign all data to object using a mapper
                gameToUpdate = MapGameTF.GameDOtoPO(item);
                //establish list to hold all data on all players as recovered by the DAL access call
                List<PlayerDO> players = _playerAccess.PlayerReadAll();
                //declare list to hold relevant data from full DAL call
                List<SelectListItem> options = new List<SelectListItem>();
                foreach (PlayerDO dataObject in players)
                {
                    //Declare list to hold player names
                    SelectListItem option = new SelectListItem();
                    //make list aspect equal dataObject variable
                    option.Text = dataObject.Name;
                    //make list aspect equal dataObject variable
                    option.Value = dataObject.PlayerId.ToString();
                    //take object "option", and add it to list "options"
                    options.Add(option);
                }
                //set object value of PlayersDropDown equal to values of "options"
                gameToUpdate.PlayersDropDown = options;
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
            return View(gameToUpdate);
        }

        //Determines whether information is being taken in, or sent out
        [HttpPost]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(2, 3)]
        //Method to pass information into the database and update the necessary game
        public ActionResult UpdateGame(GamePO form)
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
                    GameDO dataObject = MapGameTF.GamePOtoDO(form);
                    //calling on a DAL access field to allow us to use a specific method within, while passing in the DO object
                    _dataAccess.GameUpdate(dataObject);
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
                result = RedirectToAction("Index", "Game");
            }
            //section of code that runs if the form is not valid
            else
            {
                //assigning a value to our variable that will be used if the form isn't valid
                result = View(form);
            }
            //runs the portion of code that is necessary for the situation
            return result;
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(3)]
        //Method for deleting a game from the database
        public ActionResult DeleteGame(int gameId)
        {
            //Beginning of processes
            try
            {
                //using DAL access to call on the game delete method, and passing in the game id
                _dataAccess.GameDelete(gameId);
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
            //redirects to the specified page 
            return RedirectToAction("Index", "Game");
        }
    }
}