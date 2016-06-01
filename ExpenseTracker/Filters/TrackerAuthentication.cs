using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ExpenseTracker.Filters
{
    public class TrackerAuthentication : ActionFilterAttribute
    {
        /// <summary>
        /// This filter authenticate user on each and every request.
        /// </summary>
        /// <param name="filterContext"> current controller</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var emptyresult = false;
            var CurrentControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var CurrentActionName = filterContext.ActionDescriptor.ActionName;
            string[] NoValidationForActions = new string[] { "index", "login", "signin", "GetSignUpCode".ToLower(), "GetRandomString".ToLower(), "signup" };
            if (!NoValidationForActions.Contains(CurrentActionName.ToLower()))
            {
                if (filterContext.HttpContext.Session["User"] != null)
                {
                }
                else if (!filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = RedirectionResult("ExpenseTracker", "LogIn");
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = 206;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.Result = new EmptyResult();
                    emptyresult = true;
                }
            }
            if (!emptyresult)
                base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Redirect from one action to another
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="ErrorID"></param>
        /// <returns></returns>
        public RedirectToRouteResult RedirectionResult(string controller, string action, string ErrorID)
        {
            return new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "controller", controller }, { "action", action }, { "area", "" }, { "Error", ErrorID } });
        }

        /// <summary>
        ///Redirect from one action to another
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public RedirectToRouteResult RedirectionResult(string controller, string action)
        {
            return new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "controller", controller }, { "action", action }, { "area", "" } });
        }
    }
}