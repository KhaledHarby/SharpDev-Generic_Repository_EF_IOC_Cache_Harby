using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Cache.MemCache
{
    public interface IConfigCachingProviderFactory
    {
        ICachingProvider GetInstance();
    }

    public abstract class ConfigCachingProviderFactory : IConfigCachingProviderFactory
    {
        protected ICachingProviderConfiguration CachingProviderConfiguration;

        protected ConfigCachingProviderFactory(ICachingProviderConfiguration config)
        {
            CachingProviderConfiguration = config;
        }

        public abstract ICachingProvider GetInstance();
    }
}
