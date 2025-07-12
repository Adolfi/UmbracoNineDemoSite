using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Controllers;
using Umbraco.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.ContactForm;

/// <summary>
/// Documentation: https://our.umbraco.com/documentation/reference/templating/mvc/forms/tutorial-partial-views
/// </summary>
public class ContactFormController(
    IUmbracoContextAccessor umbracoContextAccessor,
    IUmbracoDatabaseFactory databaseFactory,
    ServiceContext services,
    AppCaches appCaches,
    IProfilingLogger profilingLogger,
    IPublishedUrlProvider publishedUrlProvider
        ) : BaseSurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{

    [HttpPost]
    public IActionResult Submit([Bind(Prefix = "formModel")] ContactFormModel model)
    {
        if (!ModelState.IsValid)
        {
            // If the model is not valid, return the view with the model to show validation errors.
            return CurrentUmbracoPage();
        }

        //if (CurrentPage?.Children().FirstOrDefault() is IPublishedContent firstChild)
        //{
        //    return RedirectToUmbracoPage(firstChild);
        //}

        // Do some emails sending magic here, not relevant for this demo.
        TempData.Add(TempDataKey.ResponseMessage, $"Thank you {model.Name}!");
        return RedirectToCurrentUmbracoPage();
    }
}
