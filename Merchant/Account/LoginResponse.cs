using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Account
{
    public class LoginResponse
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
    }
}
