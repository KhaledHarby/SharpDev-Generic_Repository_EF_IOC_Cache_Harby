using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ioc.Ninject
{
    public static class NinjectRepositoryExtensions
    {

        public static void BindSharpRepository(this IKernel kernel, ISharpRepositoryConfiguration configuration)
        {
            kernel.Bind(typeof(IRepository<>)).ToMethod(context =>
            {
                var genericArgs = context.Request.Service.GetGenericArguments();

                return RepositoryFactory.GetInstance(genericArgs[0], configuration);
            });

            kernel.Bind(typeof(IRepository<,>)).ToMethod(context =>
            {
                var genericArgs = context.Request.Service.GetGenericArguments();

                return RepositoryFactory.GetInstance(genericArgs[0], genericArgs[1], configuration);
            });

            kernel.Bind(typeof(ICompoundKeyRepository<,,>)).ToMethod(context =>
            {
                var genericArgs = context.Request.Service.GetGenericArguments();

                return RepositoryFactory.GetInstance(genericArgs[0], genericArgs[1], genericArgs[2], configuration);
            });
        }
    }
}
