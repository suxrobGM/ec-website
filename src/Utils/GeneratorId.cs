using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Utils
{
    public static class GeneratorId
    {
        public static string GenerateLong(string prefix)
        {
            return $"{prefix}_{DateTime.Now.ToString("yyyyMMddHHmmssffffff")}";
        }

        public static string GenerateLong()
        {
            return $"{DateTime.Now.ToString("yyyyMMddHHmmssffffff")}";
        }

        public static string GenerateShort(string prefix)
        {
            return $"{DateTime.Now.ToString("ffffff")}";
        }

        public static string GenerateShort()
        {
            return $"{DateTime.Now.ToString("ffffff")}";
        }

        public static string GenerateComplex()
        {
            return Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }
    }
}
