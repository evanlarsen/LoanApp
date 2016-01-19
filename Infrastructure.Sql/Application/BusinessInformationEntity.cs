using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Application
{
    public class BusinessInformationEntity
    {
        public string Name { get; set; }
        public string DbaName { get; set; }
        public string Address { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string TaxId { get; set; }
        public string BusinessStartDate { get; set; }
        public string LengthOfOwnership { get; set; }
        public string IndustryType { get; set; }
        public string LegalEntity { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Landlord { get; set; }
        public string Rent { get; set; }
        public string LeaseExpiration { get; set; }
        public string LandlordName { get; set; }
        public string LandlordPhone { get; set; }
        public string LandlordFax { get; set; }
    }
}
