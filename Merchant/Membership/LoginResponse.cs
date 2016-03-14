using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Membership
{
    public class LoginResponse
    {
        public bool isAuthenticated;
        public string message;
        public Guid? id;

        public static LoginResponse AccountNotFound()
        {
            return new LoginResponse() { isAuthenticated = false, message = "No account found with that email" };
        }
    }
}
