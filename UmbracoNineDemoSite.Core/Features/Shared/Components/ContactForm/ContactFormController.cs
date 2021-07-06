using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.ActionResults;
using Umbraco.Cms.Web.Website.Controllers;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.ContactForm
{
    /// <summary>
    /// Documentation: https://our.umbraco.com/documentation/reference/templating/mvc/forms/tutorial-partial-views
    /// </summary>
    public class ContactFormController : SurfaceController
    {
        public ContactFormController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider) { }

        [HttpPost]
        public RedirectToUmbracoPageResult Submit(ContactForm model)
        {
            // Do some emails sending magic here, not relevant for this demo.

            TempData.Add("CustomMessage", "Thank you!");
            return RedirectToCurrentUmbracoPage();
        }
    }
}
