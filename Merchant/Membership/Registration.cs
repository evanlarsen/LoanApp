using Infrastructure;
using Infrastructure.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Membership
{
    public class Registration
    {
        readonly Guid id;
        public string email { get; private set; }
        public string phoneNumber { get; private set; }
        public string passwordHash { get; private set; }

        public Guid? emailConfirmationKey { get; private set; }
        public bool hasConfirmedEmail { get; private set; }

        public Registration(Guid id)
        {
            this.id = id;
        }

        public Registration(
            Guid id, 
            string email, 
            string phoneNumber, 
            string passwordHash, 
            Guid? emailConfirmationKey, 
            bool hasConfirmedEmail)
        {
            this.id = id;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.passwordHash = passwordHash;
            this.emailConfirmationKey = emailConfirmationKey;
            this.hasConfirmedEmail = hasConfirmedEmail;
        }

        public async Task<CommonValidationResponse> ApplyForRegistration(
            string email,
            string password,
            string phoneNumber,
            IAccountService accountService,
            IEmailService emailService)
        {
            var validationResponse = await ValidateRegistration(email, password, phoneNumber, accountService);

            if (validationResponse.anyErrorMessages)
            {
                return validationResponse;
            }

            this.email = email;
            this.passwordHash = Cryptography.CreateHash(password);
            this.phoneNumber = phoneNumber;
            this.emailConfirmationKey = Guid.NewGuid();
            await emailService.Send(email, "new account", this.emailConfirmationKey.ToString());

            return CommonValidationResponse.Ok();
        }

        public bool IsEmailConfirmed(Guid key)
        {
            if (this.emailConfirmationKey.Equals(key))
            {
                this.hasConfirmedEmail = true;
            }
            return this.hasConfirmedEmail;
        }

        async Task<CommonValidationResponse> ValidateRegistration(
            string email,
            string password,
            string phoneNumber,
            IAccountService accountService)
        {
            var commonValidationResponse = new CommonValidationResponse();
            var sharedValidation = new SharedValidation();
            var emailValidationResponse = await sharedValidation.IsValidEmailFormat(email, accountService);
            var passwordValidationResponse = sharedValidation.IsValidPassword(password);
            var phoneNumberValidationResponse = IsValidPhoneNumber(phoneNumber);

            commonValidationResponse.JoinValidationResponses(emailValidationResponse, passwordValidationResponse, phoneNumberValidationResponse);
            return commonValidationResponse;
        }

        CommonValidationResponse IsValidPhoneNumber(string phoneNumber)
        {
            var response = new CommonValidationResponse();
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                response.AddErrorMessages("phoneNumber", "Phone Number is a required field.");
            }
            return response;
        }
    }
}
