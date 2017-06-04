using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ioc.SharpIoc
{
    public class Container : IContainer
    {

        private readonly Dictionary<MappingKey, Func<object>> _mappings;

        public Container()
        {
            _mappings = new Dictionary<MappingKey, Func<object>>();
        }

        public void Register(Type type, Func<object> createInstance, string instanceName = null)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (createInstance == null)
                throw new ArgumentNullException("createInstanceDelegate");


            var key = new MappingKey(type, instanceName);

            if (_mappings.ContainsKey(key))
            {
                const string errorMessageFormat = "The requested mapping already exists - {0}";
                throw new InvalidOperationException(string.Format(errorMessageFormat, key.ToTraceString()));
            }


            _mappings.Add(key, createInstance);
        }

        public void Register<T>(Func<T> createInstance, string instanceName = null)
        {
            if (createInstance == null)
                throw new ArgumentNullException("createInstanceDelegate");


            Register(typeof(T), createInstance as Func<object>, instanceName);
        }

        public bool IsRegistered(Type type, string instanceName = null)
        {
            if (type == null)
                throw new ArgumentNullException("type");


            var key = new MappingKey(type, instanceName);

            return _mappings.ContainsKey(key);
        }

        public bool IsRegistered<T>(string instanceName = null)
        {
            return IsRegistered(typeof(T), instanceName);
        }

        public object Resolve(Type type, string instanceName = null)
        {
            var key = new MappingKey(type, instanceName);
            Func<object> createInstance;

            if (_mappings.TryGetValue(key, out createInstance))
            {
                var instance = createInstance();
                return instance;
            }

            const string errorMessageFormat = "Could not find mapping for type '{0}'";
            throw new InvalidOperationException(string.Format(errorMessageFormat, type.FullName));
        }

        public T Resolve<T>(string instanceName = null)
        {
            object instance = Resolve(typeof(T), instanceName);

            return (T)instance;
        }

        public override string ToString()
        {
            if (_mappings == null)
                return "No mappings";

            return string.Join(Environment.NewLine, _mappings.Keys);
        }
    }
}
