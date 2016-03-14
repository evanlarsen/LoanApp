using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Membership
{
    public interface IRegistrationFactory
    {
        Registration Create();
    }

    public class RegistrationFactory : IRegistrationFactory
    {
        public Registration Create()
        {
            return new Registration(Guid.NewGuid());
        }
    }
}
