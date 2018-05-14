using System.Web.Mvc;

namespace ChessPL.Controllers.ActionFilters
{
    public class SessionActionFilter : ActionFilterAttribute
    {
        
        private readonly int[] _roleCheck;

        //Setting valid values
        public SessionActionFilter(params int[] roleChecker)
        {
            _roleCheck = roleChecker;
        }
        //Method to add functionality to OnActionExecuting
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //establishing ActionResult variable and setting it equal to controller method
            ActionResult currentAction = filterContext.Result;
            //gives redirect instead of letting user go where they please
            filterContext.Result = new RedirectResult("~/Home/Index");
            //takes in User Role ID and sets it equal to the variable "role"
            int? role = (int?)filterContext.HttpContext.Session["UserRoleId"];
            //Checks if variable is null or not
            if (role != null)
            {
                //loops through each role that is valid
                foreach (int currentRole in _roleCheck)
                {
                    //checks session role against accepted valid roles
                    if (currentRole == role)
                    {
                        //allows user to proceed unimpeded
                        filterContext.Result = currentAction;
                    }
                    //if session role does not match valid role
                    else
                    { }
                }
            }
            //Runs if variable is null
            else
            { }
            //makes call back to base method
            base.OnActionExecuting(filterContext);
        }
    }
}