using System;
using System.Collections.Concurrent;
using Examine;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoElevenDemoSite.Core.Features.Shared.Constants;

namespace UmbracoElevenDemoSite.Core.Features.Search.Examine.Index
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

        private void UmbracoContextIndex_TransformingIndexValues(object? sender, global::Examine.IndexingItemEventArgs e)
        {
            var values = new Dictionary<string, IEnumerable<object>>();
            foreach (var value in e.ValueSet.Values)
            {
                switch (value.Key)
                {
                    case "path":
                        var newPathValues = new List<string>();
                        if (value.Value != null)
                        {
                            foreach (var path in value.Value)
                            {
                                if (path != null)
                                {
                                    var newPathValue = path.ToString();
                                    if (newPathValue != null)
                                        newPathValues.Add(newPathValue.Replace(",", " "));
                                }
                            }
                            values[value.Key] = newPathValues;
                        }
                        break;
                    default:
                        if (value.Value != null)
                            values[value.Key] = value.Value;
                        break;
                }
            }
            e.SetValues(values);
        }

        public void Terminate()
        {
        }
    }
}
