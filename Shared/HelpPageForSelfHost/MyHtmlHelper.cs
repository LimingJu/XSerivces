using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RazorEngine.Templating;
using RazorEngine.Text;
using SharedConfig;
using ShowMeApi_SelfHost.Areas.HelpPage;
using ShowMeApi_SelfHost.Areas.HelpPage.Models;

namespace ShowMeApi_SelfHost
{
    public class MyHtmlHelper
    {
        public IEncodedString Raw(string rawString)
        {
            return new RawString(rawString);
        }

        public HelpPageApiModel GetHelpPageApiModel(string apiDescriptionId)
        {
            return SelfHostStartupConsumeBarerToken.config.GetHelpPageApiModel(apiDescriptionId);
        }
    }

    public abstract class HtmlSupportTemplateBase<T> : TemplateBase<T>
    {
        public HtmlSupportTemplateBase()
        {
            Html = new MyHtmlHelper();
        }

        public MyHtmlHelper Html { get; set; }
    }
}
