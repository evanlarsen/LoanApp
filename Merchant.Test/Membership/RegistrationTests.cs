using Merchant.Membership;
using System;
using Xunit;

namespace Merchant.Test.Membership
{
    public class RegistrationTests
    {
        IRegistrationFactory factory;
        IAccountService accountService;
        IAccountService accountServiceEmailAlreadyTaken;
        MockEmailService emailService;
        const string email = "evan@test.com";
        const string password = "password";
        const string phoneNumber = "5555555555";

        string emailValidationCategory = "email";
        string passwordValidationCategory = "password";
        string phoneNumberValidationCategory = "phoneNumber";

        public RegistrationTests()
        {
            this.factory = new RegistrationFactory();
            this.accountService = new MockAccountService();
            this.emailService = new MockEmailService();
            this.accountServiceEmailAlreadyTaken = new MockAccountService(true);
        }

        [Fact]
        public async void ApplyForRegistrationWithInvalidEmailFormat()
        {
            var registration = this.factory.Create();
            var validationResponse = await registration.ApplyForRegistration("bad-email", password, phoneNumber, this.accountService, this.emailService);
            Assert.True(validationResponse.anyErrorMessages);
            Assert.True(validationResponse.errorMessages.ContainsKey(emailValidationCategory));
        }

        [Fact]
        public async void ApplyForRegistrationWithBlankEmail()
        {
            var registration = this.factory.Create();
            var validationResponse = await registration.ApplyForRegistration(string.Empty, password, phoneNumber, this.accountService, this.emailService);
            Assert.True(validationResponse.anyErrorMessages);
            Assert.True(validationResponse.errorMessages.ContainsKey(emailValidationCategory));
        }

        [Fact]
        public async void ApplyForRegistrationWithNonUniqueEmail()
        {
            var registration = this.factory.Create();
            var validationResponse = await registration.ApplyForRegistration(string.Empty, password, phoneNumber, this.accountServiceEmailAlreadyTaken, this.emailService);
            Assert.True(validationResponse.anyErrorMessages);
            Assert.True(validationResponse.errorMessages.ContainsKey(emailValidationCategory));
        }

        [Fact]
        public async void ApplyForRegistrationWithABlankPassword()
        {
            var registration = this.factory.Create();
            var validationResponse = await registration.ApplyForRegistration(email, string.Empty, phoneNumber, this.accountService, this.emailService);
            Assert.True(validationResponse.anyErrorMessages);
            Assert.True(validationResponse.errorMessages.ContainsKey(passwordValidationCategory));
        }

        [Fact]
        public async void ApplyForRegistrationWithAShortPassword()
        {
            var registration = this.factory.Create();
            var validationResponse = await registration.ApplyForRegistration(email, "a", phoneNumber, this.accountService, this.emailService);
            Assert.True(validationResponse.anyErrorMessages);
            Assert.True(validationResponse.errorMessages.ContainsKey(passwordValidationCategory));
        }

        [Fact]
        public async void ApplyForRegistrationWithBlankPhoneNumber()
        {
            var registration = this.factory.Create();
            var validationResponse = await registration.ApplyForRegistration(email, password, string.Empty, this.accountService, this.emailService);
            Assert.True(validationResponse.anyErrorMessages);
            Assert.True(validationResponse.errorMessages.ContainsKey(phoneNumberValidationCategory));
        }

        [Fact]
        public async void ApplyForRegistrationSuccessfullRegistration()
        {
            var registration = this.factory.Create();
            var validationResponse = await registration.ApplyForRegistration(email, password, phoneNumber, this.accountService, this.emailService);
            Assert.True(this.emailService.body.Contains(registration.emailConfirmationKey.Value.ToString()));
        }

        [Fact]
        public async void ApplyForRegistrationConfirmEmailWithIncorrectKey()
        {
            var registration = this.factory.Create();
            var validationResponse = await registration.ApplyForRegistration(email, password, phoneNumber, this.accountService, this.emailService);
            Assert.False(registration.IsEmailConfirmed(Guid.NewGuid()));
        }

        [Fact]
        public async void ApplyForRegistrationConfirmEmailWithCorrectKey()
        {
            var registration = this.factory.Create();
            var validationResponse = await registration.ApplyForRegistration(email, password, phoneNumber, this.accountService, this.emailService);
            Assert.True(registration.IsEmailConfirmed(registration.emailConfirmationKey.Value));
        }
    }
}
