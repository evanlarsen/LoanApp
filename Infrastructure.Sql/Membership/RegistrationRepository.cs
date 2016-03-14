using Merchant.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Membership
{
    public class RegistrationRepository : IRegistrationRepository
    {
        public Task<Registration> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Save(Registration registration)
        {
            throw new NotImplementedException();
        }
    }
}
