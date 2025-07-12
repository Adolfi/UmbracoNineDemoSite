using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Examine;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoDemoSite.Core.Features.Shared.Constants;

namespace UmbracoDemoSite.Core.Features.Search.Examine.Index
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

    public class UmbracoContentComponent : IAsyncComponent
    {
        private readonly IExamineManager _examineManager;

        public UmbracoContentComponent(IExamineManager examineManager)
        {
            _examineManager = examineManager;
        }

        public Task InitializeAsync(bool isRestarting, CancellationToken cancellationToken)
        {
            if (!_examineManager.TryGetIndex(IndexNames.ExternalIndex, out IIndex index))
            {
                throw new InvalidOperationException($"No index found by name {IndexNames.ExternalIndex}");
            }

            index.TransformingIndexValues += UmbracoContextIndex_TransformingIndexValues;
            return Task.CompletedTask;
        }

        private void UmbracoContextIndex_TransformingIndexValues(object? sender, IndexingItemEventArgs e)
        {
            var pathKey = "path";
            var pathValue = e.ValueSet.GetValue(pathKey)?.ToString();
            if (pathValue != null)
            {
                var newPathValue = pathValue.Replace(",", " ");

                e.SetValues(new Dictionary<string, IEnumerable<object>>() { { pathKey, new object[] { newPathValue } } });
            }
        }

        public Task TerminateAsync(bool isRestarting, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
