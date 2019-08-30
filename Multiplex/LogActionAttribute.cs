using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multiplex
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(LogActionAttribute));
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RouteData.Values["Controller"];
            var action = filterContext.RouteData.Values["Action"];

            log.Info("Action: " + action +  " Controller: "  + controller + " was initiated!");

            base.OnActionExecuting(filterContext);
        }
    }
}
