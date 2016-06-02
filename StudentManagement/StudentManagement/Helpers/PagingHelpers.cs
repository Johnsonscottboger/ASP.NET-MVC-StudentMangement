using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Helper
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo,Func<int,string> pageUrl)
        {
            int i = 1;
            StringBuilder result = new StringBuilder();

            TagBuilder tag_li_previous = new TagBuilder("li");
            TagBuilder tag_a_previous = new TagBuilder("a");
            TagBuilder tag_span_previous = new TagBuilder("span");
            tag_span_previous.MergeAttribute("aria-hidden", "true");
            tag_span_previous.InnerHtml = "&laquo;";
            if(pagingInfo.CurrentPage-1 >= 1)
            {
                tag_a_previous.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
            }
            else
            {
                tag_li_previous.AddCssClass("disabled");
            }
            tag_a_previous.MergeAttribute("aria-lable", "Previous");
            tag_a_previous.InnerHtml = tag_span_previous.ToString();
            tag_li_previous.InnerHtml = tag_a_previous.ToString();
            result.Append(tag_li_previous.ToString());

            for(; i <= pagingInfo.TotalPages;i++)
            {
                TagBuilder tag_li = new TagBuilder("li");
                TagBuilder tag_a = new TagBuilder("a");
                tag_a.MergeAttribute("href", pageUrl(i));
                tag_a.InnerHtml = i.ToString();
                if(i==pagingInfo.CurrentPage)
                {
                    tag_li.AddCssClass("active");
                    tag_a.AddCssClass("selected");
                }

                tag_li.InnerHtml = tag_a.ToString();
                result.Append(tag_li.ToString());
            }

            TagBuilder tag_li_next = new TagBuilder("li");
            TagBuilder tag_a_next = new TagBuilder("a");
            TagBuilder tag_span_next = new TagBuilder("span");
            tag_span_next.MergeAttribute("aria-hidden", "true");
            tag_span_next.InnerHtml = "&raquo;";
            if (pagingInfo.CurrentPage + 1 <= pagingInfo.TotalPages)
            {
                tag_a_next.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
            }
            else
            {
                tag_li_next.AddCssClass("disabled");
            }     
            tag_a_next.MergeAttribute("aria-lable", "Next");
            tag_a_next.InnerHtml = tag_span_next.ToString();
            tag_li_next.InnerHtml = tag_a_next.ToString();
            result.Append(tag_li_next.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}