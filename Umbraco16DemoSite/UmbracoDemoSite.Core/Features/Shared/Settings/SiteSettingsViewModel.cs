using System;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;
using UmbracoNineDemoSite.Core.Features.Shared.Content;
using UmbracoNineDemoSite.Core.Services;
using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Shared.Settings;

/// <summary>
/// Exposing simple POCO properties which are set during initialization in the constructor (ctor).
/// This allows a dependency injection scope of scoped (see: SiteSettingsComposer).
/// Thus the setup happend only once per page request, although SiteSettings are injected in several ViewComponents.
/// </summary>
public class SiteSettingsViewModel : SitePageBase, ISiteSettingsViewModel
{
    public SiteSettingsViewModel() { }
    public SiteSettingsViewModel(IPublishedContent content, IViewModelService viewModelService, IPublishedValueFallback fallback)
        : base(content, viewModelService, fallback)
    {
        var settings = new generatedModels.SiteSettings(content, fallback)
            ?? throw new InvalidOperationException("Content is not of type SiteSettings.");

        CallToActionDescription = settings.CallToActionDescription;
        CallToActionButtonLabel = settings.CallToActionButtonLabel;
        CallToActionHeader = settings.CallToActionHeader;
        CallToActionUrl = settings.CallToActionUrl;
        FooterText = settings.FooterText;
    }
    public SiteSettingsViewModel(
        IUmbracoContextAccessor umbracoContextAccessor,
        IPublishedContentQuery publishedContentQuery,
        IDocumentNavigationQueryService documentNavigationQueryService,
        IViewModelService viewModelService,
        IPublishedValueFallback publishedValueFallback)
    {
        umbracoContextAccessor
             .TryGetUmbracoContext(out IUmbracoContext? umbracoContext);

        var homeContent = viewModelService.GetRoot();
        if (homeContent == null) return;

        SiteName = homeContent.Name;
        var typeAlias = generatedModels.SiteSettings.ModelTypeAlias;
        var settingsContent = viewModelService
            .GetChildren(homeContent)?
            .FirstOrDefault(c => c.ContentType.Alias.Equals(typeAlias));

        var settings = new generatedModels.SiteSettings(settingsContent, publishedValueFallback);
        if (settings == null) { return; }

        this.Id = settings.Id;
        this.Key = settings.Key;
        this.Name = settings.Name;
        this.PageTitle = settings.Name;
        this.SiteName = homeContent?.Name;

        CallToActionDescription = settings.CallToActionDescription;
        CallToActionButtonLabel = settings.CallToActionButtonLabel;
        CallToActionHeader = settings.CallToActionHeader;
        CallToActionUrl = settings.CallToActionUrl;
        FooterText = settings.FooterText;
    }

    public string? CallToActionHeader { get; set; }
    public string? CallToActionDescription { get; set; }
    public IPublishedContent? CallToActionUrl { get; set; }
    public string? CallToActionButtonLabel { get; set; }
    public string? FooterText { get; set; }
}
