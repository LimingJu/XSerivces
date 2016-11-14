using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataProtection;

namespace SharedConfig
{
    public class XServiceDataProtector : IDataProtector
    {
        public byte[] Protect(byte[] userData)
        {
            //var debug = Encoding.UTF8.GetString(userData);
            //Console.WriteLine(debug);
            return userData;
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            //var debug = Encoding.UTF8.GetString(protectedData);
            //Console.WriteLine(debug);
            return protectedData;
        }
    }
}
