using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Api.Infrastructure
{
    public class TypeNotRegisteredWithContainerException : Exception
    {
        public readonly Type Type;
        public TypeNotRegisteredWithContainerException(Type type)
        {
            this.Type = type;
        }
    }
}
