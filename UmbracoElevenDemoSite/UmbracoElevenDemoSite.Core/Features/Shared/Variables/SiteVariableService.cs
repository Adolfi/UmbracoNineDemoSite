using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;
using UmbracoElevenDemoSite.Core.Features.Shared.Constants;

namespace UmbracoElevenDemoSite.Core.Features.Shared.Variables
{
    public class SiteVariableService : ISiteVariable
    {
        private readonly UmbracoHelper umbracoHelper;
        private Dictionary<string, object> variables;

        public SiteVariableService(UmbracoHelper umbracoHelper)
        {
            this.umbracoHelper = umbracoHelper;
            this.CollectVariables();
        }

        private void CollectVariables()
        {
            this.variables = new Dictionary<string, object>();

            var home = this.umbracoHelper?.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == ContentTypeAlias.Home);
            var settings = home?.Children?.FirstOrDefault(x => x.ContentType.Alias == ContentTypeAlias.SiteSettings);
            var siteVariables = settings?.Children.FirstOrDefault(x => x.ContentType.Alias == ContentTypeAlias.SiteVariables)?.Children;

            if (siteVariables == null) return;
            foreach (var variable in siteVariables)
            {
                variables.Add(variable.Value<string>("alias"), variable.Value("value"));
            }
        }

        public T Get<T>(string alias, T fallback)
        {
            try
            {
                var variable = this.variables.TryGetValue(alias, out var value);
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (System.Exception)
            {
                return fallback;
            }
        }
    }
}
