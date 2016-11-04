using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;
using RazorEngine.Templating;
using SharedConfig;

namespace ShowMeApi_SelfHost.Areas.HelpPage.Controllers
{
    /// <summary>
    /// This controller is for display API description info, works as a ReadMe.
    /// </summary>
    public class ShowMeApiController : ApiController
    {
        public string HelpPageRouteName = "Default";

        [System.Web.Http.HttpGet]
        public HtmlActionResult Index()
        {
            DynamicViewBag viewBag = new DynamicViewBag();
            viewBag.AddValue("Introduction", Assembly.GetEntryAssembly().FullName);
            viewBag.AddValue("DocumentationProvider", Configuration.Services.GetDocumentationProvider());
            return new HtmlActionResult("Test",
                SelfHostStartup.config.Services.GetApiExplorer().ApiDescriptions, viewBag);
        }

        //[HttpGet]
        //public virtual HttpResponseMessage Api(string apiId)
        //{
        //    if (!String.IsNullOrEmpty(apiId))
        //    {
        //        HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
        //        if (apiModel != null)
        //        {
        //            string controllerName = Regex.Replace(GetType().Name, "controller", "", RegexOptions.IgnoreCase);
        //            Api template = new Api
        //            {
        //                Model = apiModel,
        //                HomePageLink = Url.Link(HelpPageRouteName, new { controller = controllerName })
        //            };
        //            string helpPage = template.TransformText();
        //            return new HttpResponseMessage
        //            {
        //                Content = new StringContent(helpPage, Encoding.UTF8, "text/html")
        //            };
        //        }
        //    }

        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "API not found.");
        //}
    }
}
