using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace UmbracoNineDemoSite.Core.Features.Shared.Controllers
{
    public class BaseSurfaceController : SurfaceController
    {
        private readonly IUmbracoContextAccessor umbracoContextAccessor;

        public BaseSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            this.umbracoContextAccessor = umbracoContextAccessor;
        }

        /// <summary>
        /// For testability purposes. SurfaceController.CurrentPage is not testable in Umbraco.Cms.Web.Website.Controllers at the moment.
        /// </summary>
        protected override IPublishedContent CurrentPage {
            get {
                umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);
                return umbracoContext?.PublishedRequest.PublishedContent;
            }
        }
    }
}
