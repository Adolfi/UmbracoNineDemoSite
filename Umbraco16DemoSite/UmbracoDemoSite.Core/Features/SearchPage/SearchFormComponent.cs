using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using UmbracoDemoSite.Core.Features.Shared.Components.ContactForm;

namespace UmbracoDemoSite.Core.Features.SearchPage
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
