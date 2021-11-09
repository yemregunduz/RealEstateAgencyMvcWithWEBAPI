using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace RealEstateWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AcquireRequestState(Object sender, EventArgs e)
        {
            // This is the page
            string requestPath = HttpContext.Current.Request.Path;

            // Check if I am all ready on login page to avoid crash
            if (!requestPath.EndsWith("auth/login"))
            {

                // Extract the form's authentication cookie
                if (Session.Count==0)
                {
                    Response.Redirect("/auth/login", true);
                    return;
                }
                if (requestPath.ToLower().Contains("realestateclassified"))
                {
                    if (Session["userType"].ToString().Contains("admin"))
                    {
                        Response.Redirect("/Users/GetAllUsers");
                        return;
                    }
                }
                if (requestPath.ToLower().Contains("users"))
                {
                    if (Session["userType"].ToString().Contains("realestate"))
                    {
                        Response.Redirect("/realestateclassifieds");
                        return;
                    }
                }
                var token = Session["token"].ToString();
                // If not logged in
                if (token.Length==0)
                {
                    Response.Redirect("/auth/login", true);
                    Response.End();
                    return;
                }
            }
        }
    }
}
