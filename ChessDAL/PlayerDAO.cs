using CapstoneDAO.ErrorText;
using CapstoneDAO.Mapping;
using CapstoneDAO.Models;
using ChessDAL.Mapping;
using ChessDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CapstoneDAO
{
    public class PlayerDAO
    {
        //Constructor for Connection Strings
        public PlayerDAO(string con)
        {
            _conn = con;
        }
        //variable for connectioin strings
        private readonly string _conn = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
        //Method for adding a new player to the database
        public void PlayerCreate(PlayerDO player)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Beginning the processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand createPlayer = new SqlCommand("PLAYER_ADD", scon);
                //Defining what kind of command our SqlCommand is
                createPlayer.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                createPlayer.Parameters.AddWithValue("@Name", player.Name);
                createPlayer.Parameters.AddWithValue("@PeakRating", player.PeakRating);
                createPlayer.Parameters.AddWithValue("@BirthDate", player.BirthDate);
                createPlayer.Parameters.AddWithValue("@Dead", player.Dead);
                createPlayer.Parameters.AddWithValue("@CountryOfOrigin", player.CountryOfOrigin);
                createPlayer.Parameters.AddWithValue("@CountryRepresented", player.CountryRepresented);
                //telling the code to use the stored procedure
                createPlayer.ExecuteNonQuery();
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
        //Method for deleting a player form the database
        public void PlayerDelete(long playerId)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Beginning the processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand deletePlayer = new SqlCommand("PLAYER_DELETE_CASCADE", scon);
                //Defining what kind of command our SqlCommand is
                deletePlayer.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                deletePlayer.Parameters.AddWithValue("@PlayerId", playerId);
                //telling the code to use the stored procedure
                deletePlayer.ExecuteNonQuery();
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
        //Method for Reading all general information on all players in the database 
        public List<PlayerDO> PlayerReadAll()
        {
            //Establishing new list using model PlayerDO
            List<PlayerDO> players = new List<PlayerDO>();
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
                SqlCommand readRow = new SqlCommand("PLAYER_READ_ALL", scon);
                //Defining what kind of command our SqlCommand is
                readRow.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //defining our adapter and what command it uses
                adapter = new SqlDataAdapter(readRow);
                //telling code to use the data table to fill itself
                adapter.Fill(table);
                //setting our list equal to all information pulled from database by running it through a mapper
                players = PlayerListMap.DataTableToList(table);
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
            return players;
        }
        //Method for reading all more specific information on all players in the database
        public PlayerDO PlayerReadByID(long playerId)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Establishing a new data adapter
            SqlDataAdapter iDReader = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Establishing new object using model PlayerDO
            PlayerDO player = new PlayerDO();
            //Beginning the processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRowById = new SqlCommand("PLAYER_READ_BY_ID", scon);
                //Defining what kind of command our SqlCommand is
                readRowById.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                readRowById.Parameters.AddWithValue("@PlayerId", playerId);
                //defining our adapter and what command it uses
                iDReader = new SqlDataAdapter(readRowById);
                //telling the code to use the stored procedure
                readRowById.ExecuteNonQuery();
                //telling code to use the data table to fill itself
                iDReader.Fill(table);
                //setting our object equal to all information pulled from database by running it through a mapper
                player = PlayerListMap.DataTableToList(table).FirstOrDefault();
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
            return player;
        }
        //Method for updating a players information in the database
        public void PlayerUpdate(PlayerDO player)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Beginning the processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand updatePlayer = new SqlCommand("PLAYER_UPDATE", scon);
                //Defining what kind of command our SqlCommand is
                updatePlayer.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                updatePlayer.Parameters.AddWithValue("@PlayerId", player.PlayerId);
                updatePlayer.Parameters.AddWithValue("@Name", player.Name);
                updatePlayer.Parameters.AddWithValue("@PeakRating", player.PeakRating);
                updatePlayer.Parameters.AddWithValue("@BirthDate", player.BirthDate);
                updatePlayer.Parameters.AddWithValue("@Dead", player.Dead);
                updatePlayer.Parameters.AddWithValue("@CountryOfOrigin", player.CountryOfOrigin);
                updatePlayer.Parameters.AddWithValue("@CountryRepresented", player.CountryRepresented);
                //telling the code to use the stored procedure
                updatePlayer.ExecuteNonQuery();
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
        //Method for reading all more specific information on all players in the database
        public List<PlayerDO> PlayerDetails(long playerId)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Establishing a new data adapter
            SqlDataAdapter iDReader = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Establishing new list using model PlayerDO
            List<PlayerDO> player = new List<PlayerDO>();
            //Beginning the processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRowById = new SqlCommand("PLAYER_READ_BY_ID", scon);
                //Defining what kind of command our SqlCommand is
                readRowById.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                readRowById.Parameters.AddWithValue("@PlayerId", playerId);
                //defining our adapter and what command it uses
                iDReader = new SqlDataAdapter(readRowById);
                //telling the code to use the stored procedure
                readRowById.ExecuteNonQuery();
                //telling code to use the data table to fill itself
                iDReader.Fill(table);
                //setting our list equal to all information pulled from database by running it through a mapper
                player = PlayerListMap.DataTableToList(table);
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
            return player;
        }
        //Method for searching for a player by their name
        public List<PlayerDO> PlayerReadByName(string winner)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Establishing a new data adapter
            SqlDataAdapter iDReader = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Establishing new list using model PlayerDO
            List<PlayerDO> player = new List<PlayerDO>();
            //Beginning the processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRowById = new SqlCommand("READ_PLAYER_BY_NAME", scon);
                //Defining what kind of command our SqlCommand is
                readRowById.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                readRowById.Parameters.AddWithValue("@Name", winner);
                //defining our adapter and what command it uses
                iDReader = new SqlDataAdapter(readRowById);
                //telling the code to use the stored procedure
                readRowById.ExecuteNonQuery();
                //telling code to use the data table to fill itself
                iDReader.Fill(table);
                //setting our list equal to all information pulled from database by running it through a mapper
                player = PlayerListMap.DataTableToList(table);
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
            return player;
        }
        //Method for searching through all the players, counting all of their wins, and then arranging them from most to least wins
        public List<WinsDO> OrderByWinCount()
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Establishing a new data adapter
            SqlDataAdapter iDReader = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Establishing new list using model PlayerDO
            List<WinsDO> winner = new List<WinsDO>();
            //Beginning the processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readWins = new SqlCommand("ORDER_BY_WIN_COUNT", scon);
                //Defining what kind of command our SqlCommand is
                readWins.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //defining our adapter and what command it uses
                iDReader = new SqlDataAdapter(readWins);
                //telling the code to use the stored procedure
                readWins.ExecuteNonQuery();
                //telling code to use the data table to fill itself
                iDReader.Fill(table);
                //setting our list equal to all information pulled from database by running it through a mapper
                winner = WinMap.DataTableToList(table);
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
            return winner;
        }
    }
}