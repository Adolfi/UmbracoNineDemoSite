using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Content;
using UmbracoNineDemoSite.Integrations.Products.Entities;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    public class ProductPageViewModel : SitePageBase, IHeadingPage
    {
        private string _previewModePrefix;
        public ProductPageViewModel(IProduct product, string previewPath) : base()
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Description = product.Description;
            this.PageDescription = product.ShortDescription;
            this.ImageUrl = product.ImageUrl;
            this.Price = product.Price;
            _previewModePrefix = string.IsNullOrEmpty(previewPath) ? "" : $"{previewPath}/";
        }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int Price { get; set; }
        public string? UrlSegment => $"{_previewModePrefix}{Id}/{Name}";

        public string? Heading { get; set; }
        public override string? PageTitle => Name;
		public override string? PageDescription { get; set; }
    }
}
