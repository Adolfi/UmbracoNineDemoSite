using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Services;
using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Shared.Content
{
    public class SitePageBase
    {
        private readonly ISEO? seoModel;

        public SitePageBase() { }

        public SitePageBase(
            IPublishedContent content, 
            IViewModelService viewModelService, 
            IPublishedValueFallback fallback)
        {
            var root = viewModelService?.GetRoot(content);
            SiteName = root?.Name;

            seoModel = content as ISEO;
            seoModel ??= new SEO(content, fallback);
            if (seoModel != null)
            {
                Id = seoModel.Id;
                Key = seoModel.Key;
                Level = seoModel.Level;
                Name = seoModel.Name;
                PageTitle = seoModel.PageTitle;
                PageDescription = seoModel.PageDescription;
            }
        }

        public string BodyClass = "frontpage theme-font-serif theme-color-earth";

        public int Id { get; set; }
        public int Level { get; set; }
        public Guid Key { get; set; }

        public string? Name { get; set; }

        public virtual string? PageTitle { get; set; }

        public virtual string? PageDescription { get; set; }

        public string? SiteName { get; set; }
    }
}