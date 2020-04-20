using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TP2_Part1.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString CustomSubmit(this HtmlHelper html, string text)
        {
            return new HtmlString("<input type = \"submit\" value=\"" + text + "\" class=\"btn btn-default\" />");
        }
    }
}