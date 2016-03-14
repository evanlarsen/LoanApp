using System;

namespace Merchant.Api.Membership
{
    public class LoginResult
    {
        public string authenticationToken;
        public string email;
        public Guid id;
    }
}
