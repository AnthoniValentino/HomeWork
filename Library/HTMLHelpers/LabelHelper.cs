using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.HTMLHelpers
{
    public static class LabelHelper
    {
        public static IHtmlString Create_Lable(string content, string target)
        {
            string LableStr = $"<label for = {target} style=\"background-color:gray;color:yellow;font-size:24px\">{content}</label>";
            return new HtmlString(LableStr);
        }
    }
}