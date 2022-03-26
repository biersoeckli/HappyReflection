using System.Collections.Generic;
using System.IO;

namespace HappyReflection.Models
{
    public class HappyReflectionConfiguration
    {
        /// <summary>
        /// For performance reasons dlls starting with "System" or "Microsoft" will be excluded. Add/remove your own exclusions.
        /// </summary>
        public IList<string> ExcludedNamespaces { get; set; }

        /// <summary>
        /// HappyReflection searches for dlls in the current application directory. Default search option is SearchOption.AllDirectories.
        /// </summary>
        public SearchOption AssemblySearchOption { get; set; }

        public HappyReflectionConfiguration()
        {
            ExcludedNamespaces = new List<string>
            {
                "microsoft",
                "system"
            };
            AssemblySearchOption = SearchOption.AllDirectories;
        }
    }
}
