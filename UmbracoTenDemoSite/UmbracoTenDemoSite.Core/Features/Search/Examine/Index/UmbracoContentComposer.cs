using System;
using Examine;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoTenDemoSite.Core.Features.Shared.Constants;

namespace UmbracoTenDemoSite.Core.Features.Search.Examine.Index
{
    /// <summary>
    /// An example of how to subscribe to an event to transform index values.
    /// </summary>
    public class UmbracoContentComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Components().Append<UmbracoContentComponent>();
        }
    }

    public class UmbracoContentComponent : IComponent
    {
        private readonly IExamineManager _examineManager;

        public UmbracoContentComponent(IExamineManager examineManager)
        {
            _examineManager = examineManager;
        }

        public void Initialize()
        {
            if (!_examineManager.TryGetIndex(IndexNames.ExternalIndex, out IIndex index))
            {
                throw new InvalidOperationException($"No index found by name {IndexNames.ExternalIndex}");
            }

            index.TransformingIndexValues += UmbracoContextIndex_TransformingIndexValues;
        }

        private void UmbracoContextIndex_TransformingIndexValues(object sender, global::Examine.IndexingItemEventArgs e)
        {
            var pathKey = "path";
            var pathValue = e.ValueSet.GetValue(pathKey)?.ToString();
            if (pathValue != null)
            {
                var newPathValue = pathValue.Replace(",", " ");

                //ValueSet.Values are read only in Examine 3
                //e.ValueSet.Set(pathKey, newPathValue);
                var currentPath = e.ValueSet.GetValue(pathKey);
                currentPath = newPathValue;//throws exception because ValueSet.Values are read only?
            }
        }

        public void Terminate()
        {
        }
    }
}
