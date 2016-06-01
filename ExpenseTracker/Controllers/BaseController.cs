using ExpenseTracker.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    [TrackerAuthentication(Order = 0)]
    public abstract class BaseController : Controller
    {
        protected virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //string strCurrentAactionName = filterContext.ActionDescriptor.ActionName;
            //string strCurrentcONTROLLERRName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            base.OnActionExecuted(filterContext);
        }

        protected virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //string strCurrentAactionName = filterContext.ActionDescriptor.ActionName;
            //string strCurrentcONTROLLERRName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            base.OnActionExecuting(filterContext);
        }
    }
}