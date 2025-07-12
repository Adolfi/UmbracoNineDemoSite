using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;
using UmbracoNineDemoSite.Core;
using UmbracoNineDemoSite.Tests.Extensions;
using UmbracoNineDemoSite.Tests.Models;

namespace UmbracoNineDemoSite.Tests.Unit.Helper;

public class ContentHelper
{
    private static readonly List<ContentDetails> contentDetails = [];

    private delegate void ServiceTryGetUmbracoContext(out IUmbracoContext context);
    private delegate void ServiceGetPublishedContent(int id, out IPublishedContent content);
    private delegate void ServiceGetPublishedContentKeys(Guid key, out IEnumerable<Guid> keys);

    public ContentAndAccessor GetUmbracoContextAccessor<T>(IEnumerable<ContentDetails> contentDetails)
        where T : PublishedContentModel
    {
        var publishedContentCollection = new List<T>();
        var contents = new List<IPublishedContent>();
        var fallback = Mock.Of<IPublishedValueFallback>();

        foreach (ContentDetails details in contentDetails)
        {
            #region setup IUmbracoContextAccessor Mock

            #region setup content Mock

            var contentTypeMock = new Mock<IPublishedContentType>();
            contentTypeMock.Setup(s => s.Alias)
                .Returns(details.ContentTypeAlias!);

            var publishedContentType = contentTypeMock.Object;
            var publishedContentMock = new Mock<IPublishedContent>();
            publishedContentMock.Setup(s => s.Key)
                .Returns(details.Key);
            publishedContentMock.Setup(s => s.Level)
                .Returns(details.Level);
            publishedContentMock.Setup(c => c.Id)
                .Returns(details.Id);
            publishedContentMock.Setup(s => s.Name)
                .Returns(details.Name!);
            publishedContentMock.Setup(s => s.UrlSegment)
                .Returns(details.UrlSegment);
            publishedContentMock.Setup(s => s.ContentType)
                .Returns(publishedContentType);
            var publishedContent = publishedContentMock.Object;
            contents.Add(publishedContent);

            var contentModel = new Mock<T>(
                publishedContent, fallback);
            // key cannot be set here, that's why ew need the productsContainerContent mocked for the constructor
            contentModel.Setup(c => c.Id)
                .Returns(details.Id);
            contentModel.Setup(s => s.Name)
                .Returns(details.Name!);
            contentModel.Setup(s => s.UrlSegment)
                .Returns(details.UrlSegment);
            contentModel.Setup(s => s.ContentType)
                .Returns(publishedContentType);

            publishedContentCollection.Add(contentModel.Object as T);
        }
        #endregion

        var contentCacheMock = new Mock<IPublishedContentCache>();
        contentCacheMock.Setup(s => s.GetById(It.IsAny<int>()))
            .Returns((int id) => publishedContentCollection.FirstOrDefault(m => m.Id == id));
        contentCacheMock.Setup(s => s.GetById(It.IsAny<Guid>()))
             .Returns((Guid key) => publishedContentCollection.FirstOrDefault(m => m.Key == key));

        var umbracoContext = new Mock<IUmbracoContext>();
        umbracoContext.Setup(s => s.Content)
            .Returns(contentCacheMock.Object);

        var umbracoContextAccessor = new Mock<IUmbracoContextAccessor>();
        umbracoContextAccessor
            .Setup(x => x.TryGetUmbracoContext(out It.Ref<IUmbracoContext?>.IsAny))
            .Callback(new ServiceTryGetUmbracoContext((out IUmbracoContext uContext) =>
            {
                uContext = umbracoContext.Object;
            }));
        #endregion

        var rVal = new ContentAndAccessor
        {
            Contents = contents,
            ContentModels = [.. publishedContentCollection],
            UmbracoContextAccessor = umbracoContextAccessor.Object
        };

        var contenDetails = GetContenDetails();
        foreach (var detail in contenDetails)
        {
            var detailsParentKey = detail.ParentKey ?? Guid.Empty;
            if (detailsParentKey != Guid.Empty)
            {
                var map = rVal.ChildrenParentKeyMaps
                    .FirstOrDefault(m => m.ParentKey == detailsParentKey);
                if (map == null)
                {
                    map = new ChildrenParentKeyMap(detailsParentKey, [detail.Key]);
                    rVal.ChildrenParentKeyMaps.Add(map);
                }
                else
                {
                    map.ChildKeys.Add(detail.Key);
                }
            }
        }

        return rVal;
    }

    public IDocumentNavigationQueryService GetDocumentNavigationServiceMock(
        bool rootKeyNull = false,
        bool productsContainerKeyNull = false,
        Guid? rootKey = null,
        Guid? productsContainerKey = null,
        IEnumerable<ChildrenParentKeyMap>? childrenParentKeyMaps = null)
    {
        var documentNavigationQueryService = new Mock<IDocumentNavigationQueryService>();

        if (rootKey != null)
        {
            documentNavigationQueryService
                .Setup(s => s.TryGetRootKeys(out It.Ref<IEnumerable<Guid>>.IsAny))
                .Returns((out IEnumerable<Guid> keys) =>
                {
                    keys = rootKeyNull ? Array.Empty<Guid>() : [rootKey.GetValueOrDefault()];
                    return !rootKeyNull;
                });
        }

        if (productsContainerKey != null)
        {
            documentNavigationQueryService
                .Setup(s => s.TryGetDescendantsKeysOfType(
                    It.IsAny<Guid>(),
                    ProductsContainer.ModelTypeAlias,
                    out It.Ref<IEnumerable<Guid>>.IsAny))
                .Returns((Guid key, string modelTypeAlias, out IEnumerable<Guid> keys) =>
                {
                    if (key == rootKey)
                    {
                        keys = productsContainerKeyNull ? Array.Empty<Guid>() : [productsContainerKey.GetValueOrDefault()];
                        return true;
                    }

                    keys = Array.Empty<Guid>();
                    return false;
                });
        }

        if (childrenParentKeyMaps != null)
        {
            documentNavigationQueryService
                .Setup(s => s.TryGetChildrenKeys(
                    It.IsAny<Guid>(),
                    out It.Ref<IEnumerable<Guid>>.IsAny))
                .Callback(new ServiceGetPublishedContentKeys((Guid key, out IEnumerable<Guid> keys) =>
                {
                    var map = childrenParentKeyMaps?.FirstOrDefault(c => c.ParentKey == key);
                    keys = map?.ChildKeys ?? [];
                }));
        }

        return documentNavigationQueryService.Object;
    }

    public List<ContentDetails> GetContenDetails()
    {
        if (contentDetails.Count == 0)
        {
            contentDetails.AddRange([
                new (1000, Guid.Parse("{05d1e9ba-6152-408f-a214-cbc06e585167}"),
                "Home", "home", Page.ModelTypeAlias, 1),
            new (1001, Guid.Parse("{a7930235-1451-4fff-aa81-45ad9a2e508e}"),
                "About us", "about-us", Page.ModelTypeAlias, 2),
            new (1002, Guid.Parse("{60afaf3b-a6e8-4e07-b5b1-8deccd99aef7}"),
                "Products", "products", Page.ModelTypeAlias, 2),
            new (1003, Guid.Parse("{6f606cf8-4c03-4db4-b83d-46ede2b4bd94}"),
                "Contact Us", "contact-us", Page.ModelTypeAlias, 2),
            new (1004, Guid.Parse("{32b8d7ab-a10a-4709-a411-7d675d3beacd}"),
                "Search", "search", SearchPage.ModelTypeAlias, 2),
            new (1005, Guid.Parse("{bd8b9ada-3b42-42d7-bfc2-3e4fc44091b8}"),
                "Settings", "settings", SiteSettings.ModelTypeAlias, 2)
                ]);

            var parentKey = contentDetails.FirstOrDefault(c => c.Id == 1000)?.Key;
            foreach (var detail in contentDetails.Where(d => d.Level == 2))
            { detail.ParentKey = parentKey; }

            parentKey = contentDetails.FirstOrDefault(c => c.Id == 1001)?.Key;
            contentDetails.AddRange([
                new (1011, Guid.Parse("{8d0aceff-0827-4324-84a2-961f4c6acd8f}"),
                "First Subpage", "first-subpage", Page.ModelTypeAlias, 3, parentKey),
            new (1012, Guid.Parse("{8a2b02de-162b-439f-a620-506573088279}"),
                "Second Subpage", "second-subpage", Page.ModelTypeAlias, 3, parentKey)
            ]);

            parentKey = contentDetails.FirstOrDefault(c => c.Id == 1005)?.Key;
            contentDetails.Add(new(1051, Guid.Parse("{e7de5083-5cc1-495f-8f8a-4caa998f6b56}"),
                "Variables", "variables", SiteVariables.ModelTypeAlias, 3, parentKey));

            parentKey = contentDetails.FirstOrDefault(c => c.Id == 1051)?.Key;
            contentDetails.AddRange([
                new(1511, Guid.Parse("{7ca797f8-cfa9-4919-b964-2dd480633921}"),
                "Enable E-Commerce (bool)", "enable-e-commerce-bool", SiteVariable.ModelTypeAlias, 4, parentKey),
                //new(1512, Guid.Parse("{0ef7552b-e772-4501-a2c3-c560529f0632}"),
                //"Maintenance Banner (html)", "maintenance-banner-html", SiteVariable.ModelTypeAlias, 4, parentKey)
                ]);
        }

        return contentDetails;
    }
}
