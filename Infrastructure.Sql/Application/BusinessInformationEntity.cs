using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Application
{
    public class BusinessInformationEntity
    {
        public LegalNameEntity LegalName { get; set; }
        public AddressEntity Address { get; set; }
        public PhoneNumbersEntity PhoneNumbers { get; set; }
        public TaxInfoEntity TaxInfo { get; set; }
        public OnlinePresenceEntity OnlinePresence { get; set; }
        public MortgageEntity Mortgage { get; set; }
    }

    public class LegalNameEntity
    {
        public string Name { get; set; }
        public string DbaName { get; set; }
    }

    public class TaxInfoEntity
    {
        public string TaxId { get; set; }
        public string StartDate { get; set; }
        public string LengthOfOwnership { get; set; }
        public string LegalEntity { get; set; }
        public string IndustryType { get; set; }
    }

    public class OnlinePresenceEntity
    {
        public string Email { get; set; }
        public string Website { get; set; }
    }

    public class MortgageEntity
    {
        public string Landlord { get; set; }
        public string Rent { get; set; }
        public string LeaseExpiration { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
    }
}
