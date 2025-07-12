using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;
using UmbracoDemoSite.Tests.Models;
using UmbracoDemoSite.Tests.Unit.Helper;
using UmbracoDemoSite.Core;
using UmbracoDemoSite.Core.Features.Shared.Content;
using UmbracoDemoSite.Core.Models;
using UmbracoDemoSite.Core.Services;
using UmbracoDemoSite.Tests.Extensions;
using static Umbraco.Cms.Core.Constants.Conventions;

namespace UmbracoDemoSite.Tests.Unit.Features;

public class BaseViewModelController
{
    private delegate void ServiceTryGetValueFallback(IPublishedProperty property, string culture, string segment, Fallback fallback, object defaultValue, out object? rVal);
    private delegate void ServiceTryGetValueFallback<T>(IPublishedElement property, string alias, string culture, string segment, Fallback fallback, T defaultValue, out T? value);

    protected Mock<IViewModelService> viewModelServiceMock = new();
    protected IPublishedContent root;
    protected Mock<IPublishedContent> rootMock;
    protected IViewModelService viewModelService;
    protected IPublishedValueFallback fallback;

    protected readonly ContentHelper contentHelper = new();
    protected readonly List<ContentDetails> contentDetails = [];
    protected readonly ContentAndAccessor contentAndAccessor;
    protected IUmbracoContextAccessor? umbracoContextAccessor;
    protected IDocumentNavigationQueryService documentNavigationQueryService;
    protected IPublishedContentQuery publishedContentQuery;

    protected List<IPublishedContent> topItems = [];
    protected List<IPublishedContent> allPages = [];

    public BaseViewModelController()
    {
        var fallbackMock = new Mock<IPublishedValueFallback>();
        fallbackMock
            .Setup(f => f.TryGetValue(It.IsAny<IPublishedProperty>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Fallback>(), It.Ref<object>.IsAny, out It.Ref<object?>.IsAny))
            .Callback(new ServiceTryGetValueFallback((IPublishedProperty property, string culture, string segment, Fallback fallback, object defaultValue, out object? rVal) =>
                {
                    rVal = null;
                }));
        fallbackMock
            .Setup(f => f.TryGetValue<object>(It.IsAny<IPublishedElement>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Fallback>(), It.Ref<object>.IsAny, out It.Ref<object?>.IsAny))
            .Callback(new ServiceTryGetValueFallback<object>((IPublishedElement property, string alias, string culture, string segment, Fallback fallback, object defaultValue, out object? value) =>
                {
                    value = null;
                }));

        fallback = fallbackMock.Object;

        contentDetails = contentHelper.GetContenDetails();

        contentAndAccessor = contentHelper.GetUmbracoContextAccessor<PublishedContentModel>(contentDetails);
        umbracoContextAccessor = contentAndAccessor?.UmbracoContextAccessor;
        root = contentAndAccessor?.Contents.FirstOrDefault(c => c.Id == 1000)
            ?? throw new InvalidOperationException("Root content not found.");

        rootMock = Mock.Get(root);

        allPages = [.. contentAndAccessor.Contents];
        topItems = [.. contentAndAccessor.Contents.Where(c => c.Level < 3)];

        viewModelServiceMock.Setup(v => v.GetRoot(It.IsAny<IPublishedContent>()))
            .Returns((IPublishedContent content) =>
            {
                return rootMock.Object;
            });
        viewModelServiceMock.Setup(v => v.MapSitePageBase(It.IsAny<SitePageBase>(), It.IsAny<ISEO>()))
            .Callback<SitePageBase, ISEO>((pageBase, currentModel) =>
            {
                pageBase.Id = currentModel.Id;
                pageBase.Name = currentModel.Name;
                pageBase.PageTitle = currentModel.PageTitle ?? currentModel.Name;
                pageBase.PageDescription = currentModel.PageDescription;
                pageBase.SiteName = viewModelServiceMock.Object.GetRoot(currentModel)?.Name ?? "Mock Sitename";
            });
        viewModelServiceMock
            .Setup(v => v.GetChildren(It.IsAny<IPublishedContent>()))
            .Returns((IPublishedContent parent) =>
                allPages.Where(c => contentDetails.FirstOrDefault(d => d.Key == c.Key)?.ParentKey == parent.Key));
        viewModelService = viewModelServiceMock.Object;

        documentNavigationQueryService = contentHelper.GetDocumentNavigationServiceMock(childrenParentKeyMaps: contentAndAccessor.ChildrenParentKeyMaps);
        var publishedContentQueryMock = new Mock<IPublishedContentQuery>();
        publishedContentQueryMock
            .Setup(s => s.Content(It.IsAny<IEnumerable<Guid>>()))
            .Returns((IEnumerable<Guid> keys) =>
                contentAndAccessor.ContentModels
                .Where(c => keys.Contains(c.Key))
                .ToList());
        publishedContentQuery = publishedContentQueryMock.Object;
    }
}
