using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http.SelfHost.Channels;

namespace SharedConfig
{
    public class HttpsSelfHostConfiguration : HttpSelfHostConfiguration
    {
        public HttpsSelfHostConfiguration(string baseAddress) : base(baseAddress) { }
        public HttpsSelfHostConfiguration(Uri baseAddress) : base(baseAddress) { }
        protected override BindingParameterCollection OnConfigureBinding(HttpBinding httpBinding)
        {
            httpBinding.Security.Mode = HttpBindingSecurityMode.Transport;
            httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            httpBinding.ConfigureTransportBindingElement = element => element.AuthenticationScheme = AuthenticationSchemes.Negotiate;
            return base.OnConfigureBinding(httpBinding);
        }
    }
}
