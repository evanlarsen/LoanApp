using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Application
{
    public class PrincipalOwnerEntity
    {
        public string Owner { get; set; }
        public string Title { get; set; }
        public string Ownership { get; set; }
        public AddressEntity Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string DateOfBirth { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string BusinessStartDate { get; set; }
        public string LengthOfOwnership { get; set; }
    }
}
