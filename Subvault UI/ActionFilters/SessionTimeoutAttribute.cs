using System.Web;
using System.Web.Mvc;

namespace Subvault_UI.ActionFilters {

    public class SessionTimeoutAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["Login"] == null) {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}