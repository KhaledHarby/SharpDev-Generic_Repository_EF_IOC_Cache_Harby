using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Cache.MemCache
{
    public interface ICachingProviderConfiguration
    {
        string Name { get; set; }
        Type Factory { get; set; }
        IDictionary<string, string> Attributes { get; set; }
        string this[string key] { get; }

        ICachingProvider GetInstance();
    }
}
