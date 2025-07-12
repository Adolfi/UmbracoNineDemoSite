using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using UmbracoNineDemoSite.Core.Features.Shared.Components.ContactForm;

namespace UmbracoNineDemoSite.Core.Features.SearchPage
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
