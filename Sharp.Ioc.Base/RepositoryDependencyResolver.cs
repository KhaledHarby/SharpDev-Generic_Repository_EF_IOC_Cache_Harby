using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ioc.Base
{
    public static class RepositoryDependencyResolver
    {
        static RepositoryDependencyResolver()
        {
            Current = null;
        }

        public static IRepositoryDependencyResolver Current { get; private set; }

        public static void SetDependencyResolver(IRepositoryDependencyResolver resolver)
        {
            Current = resolver;
        }
    }
}
