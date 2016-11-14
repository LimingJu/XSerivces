using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace SharedConfig
{
    public class XServiceAccessTokenFormat : SecureDataFormat<AuthenticationTicket>
    {
        public XServiceAccessTokenFormat(IDataProtector protector) : base(DataSerializers.Ticket, protector, TextEncodings.Base64Url)
        {
        }
    }
}
