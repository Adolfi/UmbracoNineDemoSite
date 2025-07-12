using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoNineDemoSite.Tests.Models
{
    public class ChildrenParentKeyMap
    {
        public ChildrenParentKeyMap() { }
        public ChildrenParentKeyMap(Guid parentKey, IEnumerable<Guid> childKeys)
        {
            ParentKey = parentKey;
            ChildKeys.AddRange(childKeys);
        }
        public Guid ParentKey { get; set; }
        public List<Guid> ChildKeys { get; set; } = [];
    }
}
