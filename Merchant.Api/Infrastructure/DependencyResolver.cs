using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace Merchant.Api.Infrastructure
{
    public class DependencyResolver : IDependencyResolver
    {
        readonly DiContainer container;

        public DependencyResolver(DiContainer container)
        {
            if (container == null) { throw new ArgumentNullException("container"); }
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            // nothing to release
            // the container does not hold references 
            // to created types so they are garbage collected 
            // when they go out of scope
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.container.Resolve(serviceType);
            }
            catch (TypeNotRegisteredWithContainerException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.container.ResolveAll(serviceType);
            }
            catch (TypeNotRegisteredWithContainerException)
            {
                return new object[] { };
            }
        }
    }
}
