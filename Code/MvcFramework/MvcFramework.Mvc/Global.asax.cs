using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Infrastructure.Core.Membership;
using MvcFramework.Biz;
using SD.Tools.OrmProfiler.Interceptor;

namespace MvcFramework.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", // Route name
                            "{controller}/{action}/{id}", // URL with parameters
                            new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                    );
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
            var authCookie = this.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return;

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            var userDataParser = new UserDataParser();
            // TODO Stuart: Figure out what to do with roles
            var wrappedUser = userDataParser.Decoder(authTicket.UserData, new string[0]);

            this.Context.User = wrappedUser;
            Thread.CurrentPrincipal = wrappedUser;
        }

        /// <summary>
        ///   Workaround for lame ReportViewer issue where missing images may appear in Google Chrome https://connect.microsoft.com/VisualStudio/feedback/details/556989/
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Application_BeginRequest(object sender, EventArgs e) {
            var application = sender as HttpApplication;
            if (application == null)
                return;
            var request = application.Request;

            if (request.Path.EndsWith("Reserved.ReportViewerWebControl.axd") && request.QueryString["ResourceStreamID"] != null
                && request.QueryString["ResourceStreamID"].ToLower().Contains("blank.gif"))
                this.Response.Redirect(request.ApplicationPath + "Content/Images/ssrsblank.gif");
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            MapperMaps.Initialize();

#if DEBUG
            // Don't want to run in production
            InterceptorCore.Initialize("MvcFramework");
#endif
        }
    }
}