using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI;
using RazorEngine;
using RazorEngine.Templating;
using System.Web.Mvc.Html;
using RazorEngine.Configuration;

namespace ShowMeApi_SelfHost
{
    public class HtmlActionResult : IHttpActionResult
    {
        private const string ViewDirectory = @"Areas\HelpPage\Views\Help";
        private readonly string _view;
        private readonly object _model;
        private readonly DynamicViewBag _viewBag;

        public HtmlActionResult(string viewName, dynamic model, DynamicViewBag viewBag)
        {
            _view = LoadView(viewName);
            _model = model;
            _viewBag = viewBag;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var config = new TemplateServiceConfiguration();
            // You can use the @inherits directive instead (this is the fallback if no @inherits is found).
            config.BaseTemplateType = typeof(HtmlSupportTemplateBase<>);
            using (var service = RazorEngineService.Create(config))
            {
                var parsedView = service.RunCompile(_view, "templateKey", null, _model, _viewBag);
                ; // Engine.Razor..RunCompile(ViewDirectory, "templateNameInTheCache", null, _model);
                response.Content = new StringContent(parsedView);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return Task.FromResult(response);
            }
        }

        private static string LoadView(string name)
        {
            var view = File.ReadAllText(Path.Combine(ViewDirectory, name + ".cshtml"));
            return view;
        }
    }
}
