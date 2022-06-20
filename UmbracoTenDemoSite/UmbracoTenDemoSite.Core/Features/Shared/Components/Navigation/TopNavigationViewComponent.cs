using Microsoft.AspNetCore.Mvc;

namespace UmbracoTenDemoSite.Core.Features.Shared.Components.Navigation
{
    [ViewComponent(Name = "TopNavigation")]
    public class TopNavigationViewComponent : ViewComponent
    {
        private readonly INavigationService navigationService;

        public TopNavigationViewComponent(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public IViewComponentResult Invoke(int selected)
        {
            return View(new NavigationViewModel()
            {
                Selected = selected,
                Items = this.navigationService.GetTopNavigation()
        });
        }
    }
}
