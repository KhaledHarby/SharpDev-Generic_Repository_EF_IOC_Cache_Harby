﻿using Enyim.Caching.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Cache.MemCache
{
    public class CachingProviderConfiguration : ICachingProviderConfiguration
    {
        public CachingProviderConfiguration()
        {
            Attributes = new Dictionary<string, string>();
        }

        public string Name { get; set; }

        private Type _factory;
        public Type Factory
        {
            get { return _factory; }
            set
            {
                ConfigurationHelper.CheckForInterface(value, typeof(IConfigCachingProviderFactory));

                _factory = value;
            }
        }

        public IDictionary<string, string> Attributes { get; set; }

        public ICachingProvider GetInstance()
        {
            // load up the factory if it exists and use it
            var factory = (IConfigCachingProviderFactory)Activator.CreateInstance(Factory, this);

            return factory.GetInstance();
        }

        public string this[string key]
        {
            get { return Attributes[key]; }
        }
    }
}