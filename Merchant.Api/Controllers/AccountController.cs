using Infrastructure;
using Merchant.Account;
using System.Threading.Tasks;
using System.Web.Http;

namespace Merchant.Api.Controllers
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

        [HttpPost]
        public async Task<IHttpActionResult> Register(string email, string password, string phoneNumber)
        {
            var account = this.factory.Create();
            try
            {
                await account.Register(email, password, phoneNumber, service);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            await this.repository.Save(account);
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login(string email, string password)
        {
            var account = await this.repository.Get(email);
            if (account == null)
            {
                return BadRequest("No user found with that email.");
            }
            var loginResponse = account.Login(password);
            await this.repository.Save(account);

            if (loginResponse.IsAuthenticated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(loginResponse.Message);
            }
        }
    }
}
