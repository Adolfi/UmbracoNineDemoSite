using Microsoft.AspNetCore.Mvc;

namespace UmbracoElevenDemoSite.Core.Features.Shared.Components.Navigation
{
    [ViewComponent(Name = "SubNavigation")]
    public class SubNavigationViewComponent : ViewComponent
    {
        private readonly INavigationService navigationService;

        public SubNavigationViewComponent(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public IViewComponentResult Invoke(int selected)
        {
            return View(new NavigationViewModel()
            {
                Selected = selected,
                Items = this.navigationService.GetSubNavigation(selected)
            });
        }
    }
}
