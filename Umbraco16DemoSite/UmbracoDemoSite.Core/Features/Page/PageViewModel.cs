using System.Collections.Generic;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;
using UmbracoDemoSite.Core.Features.Shared.Content;

namespace UmbracoDemoSite.Core.Features.Page;

public class PageViewModel : SitePageBase, IHeadingPage
{
    public PageViewModel() : base() { }

    public string? Heading { get; set; }

    public IHtmlEncodedString? BodyText { get; set; }

    private BlockListModel? _blocks;
    public BlockListModel? Blocks
    {
        get
        {
            return _blocks;
        }
        set
        {
            if (value != null)
            {
                _blocks = value;
            }
            else
            {
                IList<BlockListItem> emptyList = new List<BlockListItem>();
                _blocks = new BlockListModel(emptyList);
            }
        }
    }
}
