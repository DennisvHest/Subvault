using System.Web;
using System.Web.Mvc;

namespace Subvault_UI.ActionFilters {

    public class UserLoggedInAttribute : ActionFilterAttribute {

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