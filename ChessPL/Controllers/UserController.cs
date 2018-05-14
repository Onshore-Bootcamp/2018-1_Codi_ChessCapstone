using System.Collections.Generic;
using System.Web.Mvc;
using CapstoneDAO;
using CapstonePL.Models;
using CapstoneDAO.Models;
using System;
using CapstonePL.ErrorTextPL;
using System.Configuration;
using CapstonePL.Mapping;
using ChessPL.Controllers.ActionFilters;

namespace CapstonePL.Controllers
{
    public class UserController : Controller
    {
        //Constructor for assigning fields for connections
        public UserController()
        {
            //setting variable equal to the SqlConnection in appConfig
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            //setting fields equal to new variant connections
            _dataAccess = new UserDAO(connection);
        }

        //Declaration of fields for DAL access
        private UserDAO _dataAccess;

        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(2, 3)]
        //Method that pulls back general information for the view
        public ActionResult Index()
        {
            //declaring list using model UserPO
            List<UserPO> mappedUsers = new List<UserPO>();
            //Beginning of processes
            try
            {
                //declaring list using Model UserDO in order to retrieve database information
                List<UserDO> allUsers = _dataAccess.UserReadAll();
                //loop to get all objects assigned appropriately
                foreach (UserDO dataObject in allUsers)
                {
                    //assign our PO list all of the values that were in the DO list via mapper
                    mappedUsers.Add(MapUserTF.UserDOtoPO(dataObject));
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
            return View(mappedUsers);
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(0, 3)]
        //Method that adds a user 
        public ActionResult AddUser()
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
        [SessionActionFilter(0, 3)]
        //Method that passes in the User input to the database to be saved
        public ActionResult AddUser(UserPO form)
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
                    UserDO dataObject = MapUserTF.UserPOtoDO(form);
                    //calling on a DAL access field to allow us to use a specific method within, while passing in the DO object
                    _dataAccess.UserCreate(dataObject);
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
                result = RedirectToAction("UserLogin", "User");
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
        //Method that deletes a user from the database
        public ActionResult DeleteUser(int UserId)
        {
            //Beginning of processes
            try
            {
                //passes playerId in to the player delete method to delete the player assigned to the id
                _dataAccess.UserDelete(UserId);
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
            return RedirectToAction("Index", "User");
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(3)]
        //Method that allows user to input the information they want to update
        public ActionResult UpdateUser(int UserId)
        {
            //declaring object using model PlayerPO
            UserPO userToUpdate = new UserPO();
            //Beginning of processes
            try
            {
                //declare List using Model UserDO, and use it to store all information on the game recovered by using a DAL access call
                UserDO item = _dataAccess.UserReadByID(UserId);
                //assign all data to object using a mapper
                userToUpdate = MapUserTF.UserDOtoPO(item);
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
            return View(userToUpdate);
        }

        //Determines whether information is being taken in, or sent out
        [HttpPost]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(3)]
        //Method that sends the updated information to the database
        public ActionResult UpdateUser(UserPO form)
        {
            //Declaring an action result variable, but assigning it to null to reassign later
            ActionResult response = null;
            //Checks to see if the UI form is filled out correctly
            if (ModelState.IsValid)
            {
                //Beginning of processes
                try
                {
                    //passing the UI form information to run through our mapper, and assigning it to a DO object
                    UserDO dataObject = MapUserTF.UserPOtoDO(form);
                    //calling on a DAL access field to allow us to use a specific method within, while passing in the DO object
                    _dataAccess.UserUpdate(dataObject);
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
                response = RedirectToAction("Index", "User");
            }
            //section of code that runs if the form is not valid
            else
            {
                //assigns a page redirection to our variable
                response = View(form);
            }
            //runs the portion of code that is necessary for the situation
            return response;
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(3)]
        //Method that pulls back specific details on a user
        public ActionResult UserDetails(int UserId)
        {
            //declaring list using model UserPO
            List<UserPO> mappedUser = new List<UserPO>();
            //Beginning of processes
            try
            {
                //declaring list using Model GameDO in order to retrieve database information
                List<UserDO> user = _dataAccess.UserDetails(UserId);
                //loop to get all objects assigned appropriately
                foreach (UserDO dataObject in user)
                {
                    //assign our PO list all of the values that were in the DO list via mapper
                    mappedUser.Add(MapUserTF.UserDOtoPO(dataObject));
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
            return View(mappedUser);
        }

        //Determines whether information is being taken in, or sent out
        [HttpGet]
        //Method that takes in the information to determine whether or not they can log in
        public ActionResult UserLogin()
        {
            //sends user to the view
            return View();
        }

        //Determines whether information is being taken in, or sent out
        [HttpPost]
        //Method that governs user login
        public ActionResult UserLogin(LoginPO form)
        {
            //Declaring an action result variable, but assigning it to null to reassign later
            ActionResult result = View(form);
            //Checks to see if the UI form is filled out correctly
            if (ModelState.IsValid)
            {
                //Beginning of processes
                try
                {
                    //passing the UI form information to make the DAL call, run through our mapper, and assigning it to a DO object
                    LoginPO user = MapLoginTF.LoginDOtoPO(_dataAccess.ValidLogin(form.Username));
                    //Checks if the password from the user input and the password from the database associated with the username match
                    if (user.Password == form.Password)
                    {
                        //Sets the Session role id equal to the UserRoleId called back from the database
                        Session["UserRoleId"] = user.UserRoleId;
                        //Sets the Username the user inputed equal to the session username
                        Session["Username"] = user.Username;
                        //redirects the user to the specified page
                        result = RedirectToAction("Index", "Home");
                        //Sets the session timeout for 20 minutes
                        Session.Timeout = 20;
                    }
                    //if passwords do not match
                    else
                    {
                        //sets result to loop back to the form
                        result = View(form);
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
            }
            //Code that runs if the model state isn't valid
            else
            {
                //sets result to loop back to the form
                result = View(form);
            }
            //sends appropriate response back to the user
            return result;
        }
        
        //Method that logs the user out
        public ActionResult UserLogout()
        {
            //Clears the session
            Session.Clear();
            //Abandons session
            Session.Abandon();
            //redirects user to specified page
            return RedirectToAction("Index", "Home");
        }
    }
}
