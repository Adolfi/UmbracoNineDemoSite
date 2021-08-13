using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Controllers;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.ContactForm
{
    /// <summary>
    /// Documentation: https://our.umbraco.com/documentation/reference/templating/mvc/forms/tutorial-partial-views
    /// </summary>
    public class ContactFormController : BaseSurfaceController
    {
        public ContactFormController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider) { }

        [HttpPost]
        public IActionResult Submit(ContactForm model)
        {
            // Do some emails sending magic here, not relevant for this demo.
            TempData.Add(TempDataKey.ResponseMessage, $"Thank you {model.Name}!");
            return RedirectToUmbracoPage(CurrentPage);
        }
    }
}
