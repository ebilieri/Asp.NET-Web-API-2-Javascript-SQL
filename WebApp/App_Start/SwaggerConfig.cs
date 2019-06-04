using System.Web.Http;
using WebActivatorEx;
using WebApp;
using Swashbuckle.Application;
using System;
using System.Xml.XPath;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApp
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "WebApp");
                        c.IncludeXmlComments(GetXmlCommentsPath());

                        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("Exemplo de utilização do Swagger");
                        c.DocExpansion(DocExpansion.List);
                    });
        }

        private static string GetXmlCommentsPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + @"\bin\WebApp.xml";
        }
    }
}
