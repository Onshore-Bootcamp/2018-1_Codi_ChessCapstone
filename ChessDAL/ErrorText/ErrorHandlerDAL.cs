using System;
using System.IO;
using System.Configuration;

namespace CapstoneDAO.ErrorText
{
    public class ErrorHandlerDAL
    {
        //Method for recording all errors in a text file
        public static void ErrorLogger(Exception ex)
        {
            //Assigning variable for text file location
            string Error = ConfigurationManager.AppSettings["ErrorLog"];
            //Establishing a new StreamWriter
            StreamWriter Writer = new StreamWriter(Error, true);
            //Beginning of method processes
            try
            {
                //if statement that checks if an exception exists
                if (ex != null)
                {
                    //Choosing what gets recorded in the text file. "" are for a line break in the text file.
                    Writer.WriteLine("");
                    Writer.WriteLine(DateTime.Now);
                    Writer.WriteLine("Warning: {0} ", ex.Source);
                    Writer.WriteLine(ex.Message);
                    Writer.WriteLine(ex.StackTrace);
                    Writer.WriteLine("");
                }
            }
            //Catch for any errors that may happen
            catch (Exception)
            {

            }
            //finally cleans up any last loose ends
            finally
            {
                //closing StreamWriter
                Writer.Close();
                //disposing of the StreamWriter
                Writer.Dispose();
            }
        }
    }
}