using Microsoft.AspNetCore.Html;

namespace bbxp.web.mvc.Common
{
    public static class MarkdownExtension
    {
        public static HtmlString ToHtmlString(this string markdown) 
            => new(Markdig.Markdown.ToHtml(markdown));
    }
}