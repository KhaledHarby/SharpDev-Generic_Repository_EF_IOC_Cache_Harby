
using SharpDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ioc.Base
{
    public static class RepositoryFactory
    {
        private const string DefaultConfigSection = "sharpRepository";

        public static IRepository<T> GetInstance<T>(string repositoryName = null) where T : class, new()
        {
            return GetInstance<T>(repositoryName);
        }
    }

   
}
