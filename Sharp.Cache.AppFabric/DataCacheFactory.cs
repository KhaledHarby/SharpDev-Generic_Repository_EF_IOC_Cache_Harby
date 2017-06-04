using Microsoft.ApplicationServer.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Cache.AppFabric
{
    public sealed class DataCacheFactory : IDisposable
    {
        public DataCacheFactory() { }
        public DataCacheFactory(DataCacheFactoryConfiguration configuration){}

        public void Dispose(){}
        public DataCache GetCache(string cacheName) { return null; }
        public DataCache GetDefaultCache() { return null; }
    }
}
