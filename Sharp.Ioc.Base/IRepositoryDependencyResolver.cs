using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ioc.Base
{
    public interface IRepositoryDependencyResolver
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}
