using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examine;
using Examine.Search;
using UmbracoTenDemoSite.Core.Features.Search.Criteria;

namespace UmbracoTenDemoSite.Core.Features.Search.Query
{
    public abstract class BaseSearchQuery<TSearchCriteria> where TSearchCriteria : BaseSearchCriteria
    {
        protected ISearcher _searcher;

        protected BaseSearchQuery(ISearcher searcher)
        {
            _searcher = searcher;
        }

        public abstract IBooleanOperation BuildFilter(TSearchCriteria searchCriteria);
    }
}
