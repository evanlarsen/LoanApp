using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Api.Infrastructure
{
    public static class DiContainerExtensions
    {
        public static T Resolve<T>(this DiContainer container)
        {
            return (T)container.Resolve(typeof(T));
        }

        public static IEnumerable<T> ResolveAll<T>(this DiContainer container)
        {
            foreach (var type in container.ResolveAll(typeof(T)))
            {
                yield return (T)type;
            }
        }
    }
}
