using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoDemoSite.Core.Features.Search.Services;

namespace UmbracoDemoSite.Core.Features.Search
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
