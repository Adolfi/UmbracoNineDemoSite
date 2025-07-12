using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoDemoSite.Integrations.Products.Entities;
using UmbracoDemoSite.Core.Features.Shared.Constants;
using UmbracoDemoSite.Core.Features.Shared.Content;

namespace UmbracoDemoSite.Core.Features.Products
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
