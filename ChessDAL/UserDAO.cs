using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using CapstoneDAO.Models;
using System.Configuration;
using CapstoneDAO.Mapping;
using CapstoneDAO.ErrorText;
using System.Linq;

namespace CapstoneDAO
{
    public class UserDAO
    {
        //Constructor for Connection Strings
        public UserDAO(string con)
        {
            _conn = con;
        }
        //variable for connectioin strings
        private readonly string _conn = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
        //Method for adding a user to the database
        public void UserCreate(UserDO user)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Beginning the processes
            try
            {
                //opening sql connection
                scon.Open();
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand createUser = new SqlCommand("USER_ADD", scon);
                //Defining what kind of command our SqlCommand is
                createUser.CommandType = CommandType.StoredProcedure;
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                createUser.Parameters.AddWithValue("@Username", user.Username);
                createUser.Parameters.AddWithValue("@Password", user.Password);
                createUser.Parameters.AddWithValue("@LastName", user.LastName);
                createUser.Parameters.AddWithValue("@FirstName", user.FirstName);
                createUser.Parameters.AddWithValue("@Email", user.Email);
                //telling the code to use the stored procedure
                createUser.ExecuteNonQuery();
            }
            //Catch for any errors that may happen
            catch (Exception ex)
            {
                //passing in the exception that was thrown to the errorhandler
                ErrorHandlerDAL.ErrorLogger(ex);
            }
            //finally cleans up any last loose ends
            finally
            {
                //closing SqlConnection
                scon.Close();
                //disposing of the SqlConnection
                scon.Dispose();
            }
        }
        //Method for deleting a user from the database
        public void UserDelete(int UserId)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Beginning the processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand deleteUser = new SqlCommand("USER_DELETE", scon);
                //Defining what kind of command our SqlCommand is
                deleteUser.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                deleteUser.Parameters.AddWithValue("@UserId", UserId);
                //telling the code to use the stored procedure
                deleteUser.ExecuteNonQuery();
            }
            //Catch for any errors that may happen
            catch (Exception ex)
            {
                //passing in the exception that was thrown to the errorhandler
                ErrorHandlerDAL.ErrorLogger(ex);
            }
            //finally cleans up any last loose ends
            finally
            {
                //closing SqlConnection
                scon.Close();
                //disposing of the SqlConnection
                scon.Dispose();
            }
        }
        //Method for reading general information on users from database
        public List<UserDO> UserReadAll()
        {
            //Establishing new list using model UserDO
            List<UserDO> allUsers = new List<UserDO>();            
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Establishing a new data adapter
            SqlDataAdapter adapter = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Beginning the processes
            try
            {
                
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRow = new SqlCommand("USER_READ_ALL", scon);
                //Defining what kind of command our SqlCommand is
                readRow.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //defining our adapter and what command it uses
                adapter = new SqlDataAdapter(readRow);
                //telling code to use the data table to fill itself
                adapter.Fill(table);
                //setting our list equal to all information pulled from database by running it through a mapper
                allUsers = UserListMap.DataTableToList(table);
            }
            //Catch for any errors that may happen
            catch (Exception ex)
            {
                //passing in the exception that was thrown to the errorhandler
                ErrorHandlerDAL.ErrorLogger(ex);
            }
            //finally cleans up any last loose ends
            finally
            {
                //closing SqlConnection
                scon.Close();
                //disposing of the SqlConnection
                scon.Dispose();
            }
            //Sending all information pulled from the database up to the PL
            return allUsers;
        }
        //Method for reading specific information on users form the database
        public UserDO UserReadByID(int UserId)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Establishing a new data adapter
            SqlDataAdapter iDReader = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Establishing new object using model UserDO
            UserDO user = new UserDO();
            //Beginning the processes
            try
            {
                
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRowById = new SqlCommand("USER_READ_BY_ID", scon);
                //Defining what kind of command our SqlCommand is
                readRowById.CommandType = CommandType.StoredProcedure;
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                readRowById.Parameters.AddWithValue("@UserId", UserId);
                //opening sql connection
                scon.Open();
                //defining our adapter and what command it uses
                iDReader = new SqlDataAdapter(readRowById);
                //telling the code to use the stored procedure
                readRowById.ExecuteNonQuery();
                //telling code to use the data table to fill itself
                iDReader.Fill(table);
                //setting our object equal to all information pulled from database by running it through a mapper
                user = UserListMap.DataTableToList(table).FirstOrDefault();
            }
            //Catch for any errors that may happen
            catch (Exception ex)
            {
                //passing in the exception that was thrown to the errorhandler
                ErrorHandlerDAL.ErrorLogger(ex);
            }
            //finally cleans up any last loose ends
            finally
            {
                //closing SqlConnection
                scon.Close();
                //disposing of the SqlConnection
                scon.Dispose();
            }
            //Sending all information pulled from the database up to the PL
            return user;
        }
        //Method for reading specific information on users form the database
        public List<UserDO> UserDetails(int UserId)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Establishing a new data adapter
            SqlDataAdapter iDReader = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Establishing new list using model UserDO
            List<UserDO> user = new List<UserDO>();
            //Beginning the processes
            try
            {
                
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRowById = new SqlCommand("USER_READ_BY_ID", scon);
                //Defining what kind of command our SqlCommand is
                readRowById.CommandType = CommandType.StoredProcedure;
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                readRowById.Parameters.AddWithValue("@UserId", UserId);
                //opening sql connection
                scon.Open();
                //defining our adapter and what command it uses
                iDReader = new SqlDataAdapter(readRowById);
                //telling the code to use the stored procedure
                readRowById.ExecuteNonQuery();
                //telling code to use the data table to fill itself
                iDReader.Fill(table);
                //setting our list equal to all information pulled from database by running it through a mapper
                user = UserListMap.DataTableToList(table);
            }
            //Catch for any errors that may happen
            catch (Exception ex)
            {
                //passing in the exception that was thrown to the errorhandler
                ErrorHandlerDAL.ErrorLogger(ex);
            }
            //finally cleans up any last loose ends
            finally
            {
                //closing SqlConnection
                scon.Close();
                //disposing of the SqlConnection
                scon.Dispose();
            }
            //Sending all information pulled from the database up to the PL
            return user;
        }
        //Method for updating a user's information
        public void UserUpdate(UserDO user)
        {            
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Beginning the processes
            try
            {                
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand updateUser = new SqlCommand("USER_UPDATE", scon);
                //Defining what kind of command our SqlCommand is
                updateUser.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                updateUser.Parameters.AddWithValue("@UserId", user.UserId);
                updateUser.Parameters.AddWithValue("@UserRoleId", user.UserRoleId);
                updateUser.Parameters.AddWithValue("@Username", user.Username);
                updateUser.Parameters.AddWithValue("@LastName", user.FirstName);
                updateUser.Parameters.AddWithValue("@FirstName", user.LastName);
                updateUser.Parameters.AddWithValue("@Email", user.Email);
                //telling the code to use the stored procedure
                updateUser.ExecuteNonQuery();
            }
            //Catch for any errors that may happen
            catch (Exception ex)
            {
                //passing in the exception that was thrown to the errorhandler
                ErrorHandlerDAL.ErrorLogger(ex);
            }
            //finally cleans up any last loose ends
            finally
            {
                //closing SqlConnection
                scon.Close();
                //disposing of the SqlConnection
                scon.Dispose();
            }
        }
        //Method for searching the database by username, and returning username, password, roleId, and userId
        public LoginDO ValidLogin(string username)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Establishing new list using model LoginDO
            LoginDO login = new LoginDO();
            //Establishing a new data adapter
            SqlDataAdapter adapter = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Beginning the processes
            try
            {                
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand loginCheck = new SqlCommand("LOGIN_INFO_RETRIEVAL", scon);
                //Defining what kind of command our SqlCommand is
                loginCheck.CommandType = CommandType.StoredProcedure;
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                loginCheck.Parameters.AddWithValue("@Username", username);
                //defining our adapter and what command it uses
                adapter = new SqlDataAdapter(loginCheck);
                //telling code to use the data table to fill itself
                adapter.Fill(table);
                //setting our object equal to all information pulled from database by running it through a mapper
                login = LoginListMap.RowToItem(table.Rows[0]);
            }
            //Catch for any errors that may happen
            catch (Exception ex)
            {
                //passing in the exception that was thrown to the errorhandler
                ErrorHandlerDAL.ErrorLogger(ex);
            }
            //finally cleans up any last loose ends
            finally
            {
                //closing SqlConnection
                scon.Close();
                //disposing of the SqlConnection
                scon.Dispose();
            }
            //Sending all information pulled from the database up to the PL
            return login;
        }
    }
}