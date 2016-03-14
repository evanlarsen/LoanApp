using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Membership
{
    public interface IRegistrationRepository
    {
        Task Save(Registration registration);
        Task<Registration> Get(Guid id);
    }
}
