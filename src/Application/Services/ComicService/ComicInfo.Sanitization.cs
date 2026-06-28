using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Application.Services.ComicService
{
    public partial class ComicInfo
    {
        public void SanitizeStrings()
        {
            foreach (PropertyInfo prop in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.PropertyType == typeof(string) && prop.CanRead && prop.CanWrite)
                {
                    string? value = (string?)prop.GetValue(this);
                    if (value != null)
                    {
                        prop.SetValue(this, SanitizeForXml(value));
                    }
                }
            }

            if (Pages != null)
            {
                foreach (ComicPageInfo page in Pages)
                {
                    page.Key = SanitizeForXml(page.Key);
                    page.Bookmark = SanitizeForXml(page.Bookmark);
                }
            }
        }

        private static string SanitizeForXml(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            int len = value.Length;
            var sb = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                char c = value[i];
                if (IsValidXmlChar(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private static bool IsValidXmlChar(char c)
        {
            return c == 0x9 || c == 0xA || c == 0xD || (c >= 0x20 && c <= 0xD7FF) || (c >= 0xE000 && c <= 0xFFFD);
        }
    }
}
