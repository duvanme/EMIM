using Microsoft.AspNetCore.Html;
using System.IO;
using System.Text.Encodings.Web;

namespace EMIM.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string GetRawString(this IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}