using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Application
{
    public class ApplicationEntity
    {
        public Guid Id { get; set; }
        public BusinessInformationEntity BusinessInformation { get; set; }
        public FundingInformationEntity FundingInformation { get; set; }
        public PrincipalOwnerEntity Owner1 { get; set; }
        public PrincipalOwnerEntity Owner2 { get; set; }
    }
}
