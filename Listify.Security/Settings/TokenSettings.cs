using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Security.Settings
{
    public class TokenSettings
    {
        public static string SecretKey { get => "5C4F89D5FBA64F6D8ECECF011F55E53F2"; }
        public static int ExpirationInMinutes { get => 60; }
    }
}
