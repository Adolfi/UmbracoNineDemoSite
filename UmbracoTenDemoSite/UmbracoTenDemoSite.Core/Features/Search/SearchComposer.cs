using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoNineDemoSite.Core.Features.Search.Services;

namespace UmbracoNineDemoSite.Core.Features.Search
{
    public class SearchComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            var services = builder.Services;

            services.AddTransient<SearchService>();
        }
    }
}
