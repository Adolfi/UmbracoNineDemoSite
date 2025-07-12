using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;
using UmbracoDemoSite.Core.Features.Shared.Constants;

namespace UmbracoDemoSite.Core.Features.Shared.Variables
{
    public class SiteVariableService : ISiteVariable
    {
        private readonly UmbracoHelper umbracoHelper;
        private readonly Dictionary<string, object> variables = [];

        public SiteVariableService(UmbracoHelper umbracoHelper)
        {
            this.umbracoHelper = umbracoHelper;
            this.CollectVariables();
        }

        private void CollectVariables()
        {

            var home = this.umbracoHelper?.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == ContentTypeAlias.Home);
            var settings = home?.Children()?.FirstOrDefault(x => x.ContentType.Alias == ContentTypeAlias.SiteSettings);
            var siteVariables = settings?.Children().FirstOrDefault(x => x.ContentType.Alias == ContentTypeAlias.SiteVariables)?.Children();

            if (siteVariables == null) return;
            foreach (var variable in siteVariables)
            {
                var alias = variable.Value<string>("alias");
                var value = variable.Value("value");
                if (alias == null || value == null)
                {
                    continue; // Skip if alias or value is null
                }
                variables.Add(alias, value);
            }
        }

        public T Get<T>(string alias, T fallback)
        {
            try
            {
                var variable = this.variables.TryGetValue(alias, out var value);
                var rVal = Convert.ChangeType(value, typeof(T)) ?? new object();
                return (T)rVal;
            }
            catch (System.Exception)
            {
                return fallback;
            }
        }
    }
}
