using Merchant.Membership;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Login;
using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace Merchant.Api.Membership
{
    [MobileAppController]
    public class CustomAuthController : ApiController
    {
        readonly IAccountFactory factory;
        readonly IAccountRepository repository;
        readonly IAccountService service;

        public CustomAuthController(IAccountFactory factory, IAccountRepository repository, IAccountService service)
        {
            this.factory = factory;
            this.repository = repository;
            this.service = service;
        }
        
        public async Task<IHttpActionResult> Post([FromBody] LoginCredentials assertion)
        {
            var loginResponse = await IsValidAssertion(assertion);
            if (loginResponse.isAuthenticated)
            {
                JwtSecurityToken token = GetAuthenticationTokenForUser(assertion.Email);
                return Ok(new LoginResult()
                {
                    authenticationToken = token.RawData,
                    email = assertion.Email,
                    id = loginResponse.id.Value
                });
            }
            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, loginResponse.message));
            }
        }

        private JwtSecurityToken GetAuthenticationTokenForUser(string email)
        {
            var claims = new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, email) };
            string signingKey = this.GetSigningKey();
            string audience = this.GetSiteUrl();
            string issuer = this.GetSiteUrl();

            return AppServiceLoginHandler.CreateToken(
                claims,
                signingKey,
                audience,
                issuer,
                TimeSpan.FromHours(24));
        }

        private string GetSiteUrl()
        {
            var settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                return "http://localhost";
            }
            else
            {
                return "https://" + settings.HostName + "/";
            }
        }

        private string GetSigningKey()
        {
            var settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // this key is for debugging and testing purposes only
                // this key should match the one supplied in Startup.MobileApp.cs
                return "GfYVqdtZUJQfghRiaonAeRQRDjytRi47";
            }
            else
            {
                return Environment.GetEnvironmentVariable("WEBSITE_AUTH_SIGNING_KEY");
            }
        }

        private async Task<LoginResponse> IsValidAssertion(LoginCredentials assertion)
        {
            var account = await this.repository.Get(assertion.Email);
            if (account == null)
            {
                return LoginResponse.AccountNotFound();
            }
            var loginResponse = account.Login(assertion.Password);
            await this.repository.Save(account);
            return loginResponse;
        }
    }
}
