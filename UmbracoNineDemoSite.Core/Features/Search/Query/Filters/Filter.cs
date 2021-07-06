using System;
using System.Linq;
using Examine.Search;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Search.Query.Filters
{
    public static class Filter
    {
        public static IBooleanOperation FilterByAlias(this IQuery query, string[] aliases)
        {
            var fields = new[] {SearchField.NodeTypeAlias};
            if (aliases?.Any() == true)
            {
                return query.GroupedOr(fields, aliases);
            }

            return query.GroupedOr(fields, string.Empty);
        }

        public static IBooleanOperation SearchByTerm(this IBooleanOperation filter, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return filter;
            }
            var searchableFields = new[]
            {
                SearchField.Heading,
                SearchField.BodyText
            };

            var words = searchTerm.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x).ToArray();

            filter = filter.And(q => q.GroupedOr(searchableFields, words));

            return filter;
        }
    }
}
