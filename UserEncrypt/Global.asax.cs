using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UserEncrypt
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Server.MapPath("~/App_Data/logs/log-.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return;
            }
            else
            {
                string currentUrl = HttpContext.Current.Request.Url.AbsolutePath.ToLower();

                if (!currentUrl.Contains("login"))
                {
                    Response.Redirect("~/Auth/Login");
                }
            }
        }

        // Liberar recursos cuando la aplicacion se detiene
        protected void Aplication_End()
        {
            Log.CloseAndFlush();
        }
    }
}
