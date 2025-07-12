using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;
using UmbracoNineDemoSite.Integrations.Products.Services;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    /// <summary>
    /// Docs: https://our.umbraco.com/Documentation/Reference/Routing/Request-Pipeline/IContentFinder
    /// </summary>
    public class ProductsContentFinder : IContentFinder
    {
        private readonly IProductService productService;
        private readonly IUmbracoContextAccessor umbracoContextAccessor;
        public readonly IDocumentNavigationQueryService documentNavigationQueryService;

        public ProductsContentFinder(IProductService productService,
            IUmbracoContextAccessor umbracoContextAccessor,
            IDocumentNavigationQueryService documentNavigationQueryService)
        {
            this.productService = productService;
            this.umbracoContextAccessor = umbracoContextAccessor;
            this.documentNavigationQueryService = documentNavigationQueryService;
        }

        public Task<bool> TryFindContent(IPublishedRequestBuilder request)
        {
            umbracoContextAccessor
                .TryGetUmbracoContext(out IUmbracoContext? umbracoContext);
            if (umbracoContext == null)
            {
                throw new InvalidOperationException("Umbraco context is not available.");
            }
            IPublishedContent? requestContent = null;
            var inPreviewMode = umbracoContext.InPreviewMode;
            var segments = request.AbsolutePathDecoded.Split("/");
            if (segments.Length > 1 && !inPreviewMode)
            {
                if (Guid.TryParse($"{{{segments[1]}}}", out Guid contentKey))
                {
                    requestContent = umbracoContext
                        .Content
                        .GetById(contentKey);
                    if (requestContent == null || requestContent.ContentType.Alias != ProductsContainer.ModelTypeAlias)
                    {
                        return Task.FromResult(false);
                    }
                    inPreviewMode = true;
                }
            }
            int id = 0;
            if (inPreviewMode)
            {
                if (segments.Length < 3 || !int.TryParse(segments[2], out id))
                {
                    return Task.FromResult(false);
                }
            }
            else if (segments.Length > 2 && !int.TryParse(segments[2], out id))
            {
                return Task.FromResult(false);
            }

            var product = this.productService.Get(id);
            if (id > 0 && product == null)
            {
                return Task.FromResult(false);
            }

            IPublishedContent? container = requestContent;
            if (container == null)
            {
                if (!documentNavigationQueryService.TryGetRootKeys(out IEnumerable<Guid> rootKeys))
                {
                    return Task.FromResult(false);
                }
                foreach (var rootKey in rootKeys)
                {

                    if (documentNavigationQueryService
                        .TryGetDescendantsKeysOfType(rootKey, ProductsContainer.ModelTypeAlias, out IEnumerable<Guid> containerKeys))
                    {
                        var key = containerKeys.FirstOrDefault();
                        if (key == Guid.Empty)
                        {
                            continue;
                        }
                        container = umbracoContext
                            .Content
                            .GetById(key);
                        break;
                    }
                }
            }


            if (container == null)
            {
                return Task.FromResult(false);
            }

            if (umbracoContext.InPreviewMode)
            {
                if (!Guid.TryParse($"{{{segments[1]}}}", out Guid segmentKey))
                {
                    return Task.FromResult(false);
                }
                if (!segmentKey.Equals(container.Key))
                {
                    return Task.FromResult(false);
                }
            }
            else if (!segments[1].Equals(container.UrlSegment, StringComparison.InvariantCultureIgnoreCase))
            {
                return Task.FromResult(false);
            }

            request.SetPublishedContent(container);
            return Task.FromResult(true);
        }
    }
}
