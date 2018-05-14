using CapstoneDAO.ErrorText;
using CapstoneDAO.Mapping;
using CapstoneDAO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CapstoneDAO
{
    public class GameDAO
    {
        //Constructor for Connection Strings
        public GameDAO(string con)
        {
            _conn = con;
        }
        //variable for connectioin strings
        private readonly string _conn = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
        
        //Method for adding a game to database
        public void GameCreate(GameDO game)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);

            //Beginning of method processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand createGame = new SqlCommand("GAME_ADD", scon);
                //Defining what kind of command our SqlCommand is
                createGame.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();

                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                createGame.Parameters.AddWithValue("@White", game.White);
                createGame.Parameters.AddWithValue("@WhiteRating", game.WhiteRating);
                createGame.Parameters.AddWithValue("@Black", game.Black);
                createGame.Parameters.AddWithValue("@BlackRating", game.BlackRating);
                createGame.Parameters.AddWithValue("@Location", game.Location);
                createGame.Parameters.AddWithValue("@DatePlayed", game.DatePlayed);
                createGame.Parameters.AddWithValue("@Winner", game.Winner);
                //telling the code to use the stored procedure
                createGame.ExecuteNonQuery();
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
        //Method for deleting a game from the database
        public void GameDelete(long gameId)
        {
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Beginning of method processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand deleteGame = new SqlCommand("GAME_DELETE", scon);
                //Defining what kind of command our SqlCommand is
                deleteGame.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                deleteGame.Parameters.AddWithValue("@gameId", gameId);
                //telling the code to use the stored procedure
                deleteGame.ExecuteNonQuery();
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
        //Method for viewing all games with more general information
        public List<GameDO> GameReadAll()
        {
            //Establishing new list using model GameDO
            List<GameDO> allgames = new List<GameDO>();            
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Establishing a new data adapter
            SqlDataAdapter adapter = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Beginning of method processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRow = new SqlCommand("GAME_READ_ALL", scon);
                //Defining what kind of command our SqlCommand is
                readRow.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //defining our adapter and what command it uses
                adapter = new SqlDataAdapter(readRow);
                //telling code to use the data table to fill itself
                adapter.Fill(table);
                //setting our list equal to all information pulled from database by running it through a mapper
                allgames = GameListMap.DataTableToList(table);
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
            return allgames;
        }
        //Method for viewing more specific details of a game
        public GameDO GameReadByID(long gameId)
        {            
            //Defining SqlConnection for this method
            SqlConnection scon = null;
            //Establishing a new data adapter
            SqlDataAdapter iDReader = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Establishing new object using model GameDO
            GameDO game = new GameDO();
            //Beginning of method processes
            try
            {
                scon = new SqlConnection(_conn);
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRowById = new SqlCommand("GAME_READ_BY_ID", scon);
                //Defining what kind of command our SqlCommand is
                readRowById.CommandType = CommandType.StoredProcedure;
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                readRowById.Parameters.AddWithValue("@GameId", gameId);
                //opening sql connection
                scon.Open();
                //defining our adapter and what command it uses
                iDReader = new SqlDataAdapter(readRowById);
                //telling the code to use the stored procedure
                readRowById.ExecuteNonQuery();
                //telling code to use the data table to fill itself
                iDReader.Fill(table);
                //setting our object equal to all information pulled from database by running it through a mapper
                game = GameListMap.DataTableToList(table).FirstOrDefault();
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
            //Method for viewing more specific details of a game
            return game;
        }
        //Method for viewing more specific details of a game
        public List<GameDO> GameDetails(long gameId)
        {            
            //Defining SqlConnection for this method
            SqlConnection scon = null;
            //Establishing a new data adapter
            SqlDataAdapter iDReader = null;
            //Establishing a new data table
            DataTable table = new DataTable();
            //Establishing new list using model GameDO
            List<GameDO> game = new List<GameDO>();
            //Beginning of method processes
            try
            {
                scon = new SqlConnection(_conn);
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRowById = new SqlCommand("GAME_READ_BY_ID", scon);
                //Defining what kind of command our SqlCommand is
                readRowById.CommandType = CommandType.StoredProcedure;
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                readRowById.Parameters.AddWithValue("@GameId", gameId);
                //opening sql connection
                scon.Open();
                //defining our adapter and what command it uses
                iDReader = new SqlDataAdapter(readRowById);
                //telling the code to use the stored procedure
                readRowById.ExecuteNonQuery();
                //telling code to use the data table to fill itself
                iDReader.Fill(table);
                //setting our list equal to all information pulled from database by running it through a mapper
                game = GameListMap.DataTableToList(table);
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
            //Sending data we pulled back up to the PL
            return game;
        }
        //Method for updating a game by passing in the game to update, with either new or old information
        public void GameUpdate(GameDO game)
        {            
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //Beginning of method processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand updateGame = new SqlCommand("GAME_UPDATE", scon);
                //Defining what kind of command our SqlCommand is
                updateGame.CommandType = CommandType.StoredProcedure;
                //opening sql connection
                scon.Open();
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                updateGame.Parameters.AddWithValue("@GameId", game.GameId);
                updateGame.Parameters.AddWithValue("@White", game.White);
                updateGame.Parameters.AddWithValue("@WhiteRating", game.WhiteRating);
                updateGame.Parameters.AddWithValue("@Black", game.Black);
                updateGame.Parameters.AddWithValue("@BlackRating", game.BlackRating);
                updateGame.Parameters.AddWithValue("@Location", game.Location);
                updateGame.Parameters.AddWithValue("@DatePlayed", game.DatePlayed);
                updateGame.Parameters.AddWithValue("@Winner", game.Winner);
                //telling the code to use the stored procedure
                updateGame.ExecuteNonQuery();
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
        //method for reading a game by player id
        public List<GameDO> ReadGamesByPlayerId(int playerId)
        {            
            //Defining SqlConnection for this method
            SqlConnection scon = new SqlConnection(_conn);
            //establishing a data adapter
            SqlDataAdapter iDReader = null;
            //establishing a data table
            DataTable table = new DataTable();
            //establishing a new DO list
            List<GameDO> game = new List<GameDO>();
            //Beginning of method processes
            try
            {
                //Defining a variable for the SqlCommand, as well as the stored proc and connection we're using
                SqlCommand readRowById = new SqlCommand("READ_GAME_BY_PLAYER_ID", scon);
                //Defining what kind of command our SqlCommand is
                readRowById.CommandType = CommandType.StoredProcedure;
                //Adding in all the variables. Format: ("@SqlVariable", C#VariableValue)
                readRowById.Parameters.AddWithValue("@White", playerId);
                readRowById.Parameters.AddWithValue("@Black", playerId);
                //opening sql connection
                scon.Open();
                //defining the adapter as an adapter using (ThisCommand)
                iDReader = new SqlDataAdapter(readRowById);
                //telling the code to use the stored procedure
                readRowById.ExecuteNonQuery();
                //telling the adapter to fill using the data from our data table
                iDReader.Fill(table);
                //setting our list equal to the data from the table after running through a mapper to define all necessary aspects of "game"
                game = GameListMap.DataTableToList(table);
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
            //sending our data back up to the PL
            return game;
        }
    }
}