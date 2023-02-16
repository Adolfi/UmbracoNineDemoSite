using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using UmbracoElevenDemoSite.Core.Features.Shared.Components.ContactForm;

namespace UmbracoElevenDemoSite.Core.Features.SearchPage
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
