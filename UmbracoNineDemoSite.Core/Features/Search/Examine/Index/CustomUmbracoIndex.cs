using System;
using System.Collections.Generic;
using Examine;
using Examine.Lucene;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Examine;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Search.Examine.Index
{
    public class CustomUmbracoIndex : UmbracoContentIndex
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IOptionsSnapshot<LuceneDirectoryIndexOptions> _indexOptions;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRuntimeState _runtimeState;
        //private readonly ILocalizationService _languageService;
        public CustomUmbracoIndex(/*ILoggerFactory loggerFactory,*/
            IOptionsSnapshot<LuceneDirectoryIndexOptions> indexOptions,
            IHostingEnvironment hostingEnvironment,
            IRuntimeState runtimeState) : base(null, IndexNames.CustomIndex, indexOptions, hostingEnvironment, runtimeState)
        {
            //_loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _indexOptions = indexOptions ?? throw new ArgumentNullException(nameof(indexOptions));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _runtimeState = runtimeState ?? throw new ArgumentNullException(nameof(runtimeState));
        }
    }
}
