using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharedConfig
{
    public class PasswordHash
    {
        public static string GetHash(string pwd)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            return Encoding.UTF8.GetString(hash);
        }
    }
}
