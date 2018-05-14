using System.Web.Mvc;
using System.Configuration;
using CapstoneDAO;
using ChessPL.Controllers.ActionFilters;

namespace CapstonePL.Controllers
{
    public class HomeController : Controller
    {
        //Constructor for assigning fields for connections
        public HomeController()
        {
            //setting variable equal to the SqlConnection in appConfig
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            //setting fields equal to new variant connections
            _dataAccess = new UserDAO(connection);
        }

        //Declaration of fields for DAL access
        private UserDAO _dataAccess;

        //Method for viewing the Index page on the home controller
        public ActionResult Index()
        {
            //sends user to view page 
            return View();
        }

        //Method for viewing the about page
        public ActionResult About()
        {
            //sends user to view page 
            return View();
        }

        //Determines level of security, and what number UserRoleId has to be for access to method to be granted.
        [SessionActionFilter(1, 2, 3)]
        //Method for viewing the contact page
        public ActionResult Contact()
        {
            //sends user to view page 
            return View();
        }

        //Method for viewing User stories page
        public ActionResult UserStories()
        {
            //sends user to view page 
            return View();
        }
    }
}