using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Prospector.Domain.Parsers;

namespace Prospector.Presentation.Extensions
{
    public static class HtmlDropDownExtensions
    {
        public static MvcHtmlString EnumDescriptionDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, string defaultValue)
        {
            return EnumDescriptionDropDownListFor(htmlHelper, expression, defaultValue, new object());
        }

        /// Taken from - http://blogs.msdn.com/b/stuartleeks/archive/2010/05/21/asp-net-mvc-creating-a-dropdownlist-helper-for-enums.aspx
        public static MvcHtmlString EnumDescriptionDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, string defaultValue, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            IList<SelectListItem> items = (from item in values
                                           let memberInfo = item.GetType().GetMember(item.ToString())
                                           where memberInfo.Length > 0
                                           select new SelectListItem
                                           {
                                               Text = EnumParser.GetDescription(item as Enum),
                                               Value = item.ToString(),
                                               Selected = item.Equals(metadata.Model)
                                           }).ToList();

            return htmlHelper.DropDownListFor(expression, items.OrderBy(value => value.Text), defaultValue,
                htmlAttributes);
        }
    }
}