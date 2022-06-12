using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoTenDemoSite.Tests.Extensions
{
	public static class StringExtensions
	{
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            if (s.Length == 1)
            {
                return s.ToLowerInvariant();
            }

            return char.ToLowerInvariant(s[0]) + s[1..];
        }
    }
}
