using System.Web;
using System.Web.Mvc;

namespace Subvault_UI.ActionFilters {

    public class UserLoggedInAttribute : ActionFilterAttribute {

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Checks if the user is logged in before executing the action
        /// </summary>
        /// <param name="filterContext">Context</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["Login"] == null) {
                filterContext.Result = new RedirectResult("/");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}