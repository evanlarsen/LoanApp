using Infrastructure.Email;
using Merchant.Membership;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Merchant.Api.Membership
{
    public class RegistrationController : ApiController
    {
        readonly IRegistrationFactory factory;
        readonly IRegistrationRepository repository;
        readonly IAccountService accountService;
        readonly IEmailService emailService;

        public RegistrationController(
            IRegistrationFactory factory, 
            IRegistrationRepository repository, 
            IAccountService accountService,
            IEmailService emailService)
        {
            this.factory = factory;
            this.repository = repository;
            this.accountService = accountService;
        }

        public async Task<IHttpActionResult> ApplyForRegistration(string email, string password, string mobile)
        {
            var registration = this.factory.Create();
            CommonValidationResponse validationResponse = await registration.ApplyForRegistration(email, password, mobile, accountService, emailService);
            if (validationResponse.anyErrorMessages)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, validationResponse));
            }
            await this.repository.Save(registration);
            return Ok();
        }

        public async Task<IHttpActionResult> ConfirmEmailRegistration(Guid id, Guid emailRegistrationKey)
        {
            var registration = await this.repository.Get(id);
            if (!registration.IsEmailConfirmed(emailRegistrationKey))
            {
                return BadRequest();
            }
            await this.repository.Save(registration);
            return Ok();
        }
    }
}
