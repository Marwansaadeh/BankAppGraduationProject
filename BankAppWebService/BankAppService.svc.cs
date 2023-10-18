using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BankAppWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BankAppService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BankAppService.svc or BankAppService.svc.cs at the Solution Explorer and start debugging.
    public class BankAppService : IBankAppService
    {
        [AuthorizationFilter]
        string IBankAppService.GetCustomer()
        {
            return "Hello from Webapp";
        }
    }
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (HttpContext.Current.Session["UserName"] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

        }
    }
}
