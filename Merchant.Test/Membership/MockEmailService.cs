using Infrastructure.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Test.Membership
{
    internal class MockEmailService : IEmailService
    {
        public string from;
        public string subject;
        public string body;

        public Task Send(string from, string subject, string body)
        {
            this.from = from;
            this.subject = subject;
            this.body = body;
            return Task.Run(() => { });
        }
    }
}
