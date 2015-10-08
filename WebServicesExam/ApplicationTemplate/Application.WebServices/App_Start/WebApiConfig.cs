namespace Application.WebServices
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.OData.Extensions;

    using Microsoft.Owin.Security.OAuth;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.AddODataQueryFilter();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "NotificationApi",
                routeTemplate: "api/notifications",
                defaults: new { controller = "Notification" }
                );

            config.Routes.MapHttpRoute(
                name: "GuessesApi",
                routeTemplate: "api/games/{id}/guess",
                defaults: new { controller = "Guess" }
                );

            config.Routes.MapHttpRoute(
                name: "ScoreApi",
                routeTemplate: "api/scores",
                defaults: new { controller = "Score" }
                );

            config.Routes.MapHttpRoute(
                name: "GamesApi",
                routeTemplate: "api/games/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Games" }
                );

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }
    }
}