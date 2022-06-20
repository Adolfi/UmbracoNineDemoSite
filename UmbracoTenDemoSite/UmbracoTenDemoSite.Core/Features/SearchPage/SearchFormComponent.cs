using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using UmbracoTenDemoSite.Core.Features.Shared.Components.ContactForm;

namespace UmbracoTenDemoSite.Core.Features.SearchPage
{

    [ViewComponent(Name = "SearchForm")]
    public class SearchFormComponent : ViewComponent
    {
        public IViewComponentResult Invoke(SearchPageViewModel model)
        {
            return View(model.SearchForm);
        }
    }
}
