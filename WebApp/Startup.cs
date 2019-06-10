using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Swashbuckle.Application;
using System;
using System.Linq;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebApp.Startup))]

namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            //    new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSwagger(c =>
              {
                  c.SingleApiVersion("v1", "WebApp");
                  c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + @"\bin\WebApp.xml");

                  c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
              })
                 .EnableSwaggerUi(c =>
                 {
                     c.DocumentTitle("Exemplo de utilização do Swagger");
                     c.DocExpansion(DocExpansion.List);
                 });

            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(config);
        }
    }
}
