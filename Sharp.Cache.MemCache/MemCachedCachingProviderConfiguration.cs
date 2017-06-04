using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Cache.MemCache
{
    public class MemCachedCachingProviderConfiguration : CachingProviderConfiguration
    {
        public MemCachedCachingProviderConfiguration(string name, string sectionName)
        {
            Name = name;
            SectionName = sectionName;
            Factory = typeof(MemCachedConfigCachingProviderFactory);
        }

        public string SectionName
        {
            set { Attributes["sectionName"] = value; }
        }
    }
}
