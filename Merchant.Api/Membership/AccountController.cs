using Infrastructure;
using Merchant.Membership;
using System.Threading.Tasks;
using System.Web.Http;

namespace Merchant.Api.Membership
{
    public class AccountController : ApiController
    {
        readonly IAccountFactory factory;
        readonly IAccountRepository repository;
        readonly IAccountService service;

        public AccountController(IAccountFactory factory, IAccountRepository repository, IAccountService service)
        {
            this.factory = factory;
            this.repository = repository;
            this.service = service;
        }

        public async Task<IHttpActionResult> IsEmailAlreadyTaken(string email)
        {
            var isTaken = await this.service.IsEmailAlreadyTaken(email);
            if (isTaken)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }

}
