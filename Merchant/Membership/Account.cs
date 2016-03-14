using Infrastructure;
using Infrastructure.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Merchant.Membership
{
    public class Account
    {
        public readonly Guid id;
        public string email { get; private set; }
        public string passwordHash { get; private set; }
        public string phoneNumber { get; private set; }
        public DateTime lastFailedLoginAttempt { get; private set; }
        public int failedLoginAttemptCount { get; private set; }

        public const int failedLoginAttemptsBeforeDelay = 10;
        public const int maximumFailedLoginAttemptsBeforeAccountLocked = 1000;
        public readonly TimeSpan failedLoginAttemptDelay = TimeSpan.FromMinutes(5);
        
        public Account(Guid id, 
            string email, 
            string passwordHash, 
            string phoneNumber, 
            DateTime lastFailedLoginAttempt, 
            int failedLoginAttemptCount)
        {
            this.email = email;
            this.passwordHash = passwordHash;
            this.phoneNumber = phoneNumber;
            this.lastFailedLoginAttempt = lastFailedLoginAttempt;
            this.failedLoginAttemptCount = failedLoginAttemptCount;
        }

        public CommonValidationResponse ChangePassword(string oldPassword, string newPassword)
        {
            var validationResponse = new CommonValidationResponse();
            string validationCategory = "password";
            if (!Cryptography.ValidatePassword(oldPassword, this.passwordHash))
            {
                validationResponse.AddErrorMessages(validationCategory, "The old password provided was not correct");
            }
            var sharedValidation = new SharedValidation();
            validationResponse.JoinValidationResponses(sharedValidation.IsValidPassword(newPassword, validationCategory));

            if (validationResponse.anyErrorMessages)
            {
                return validationResponse;
            }

            this.passwordHash = Cryptography.CreateHash(newPassword);

            return CommonValidationResponse.Ok();
        }

        public LoginResponse Login(string attemptedPassword)
        {
            var response = new LoginResponse() { isAuthenticated = false };

            // if the user has failed 1000 times then they are probably being brute forced and their account is forcefully locked.
            if (this.failedLoginAttemptCount > maximumFailedLoginAttemptsBeforeAccountLocked)
            {
                response.message = "Account has been locked";
                FailedLoginAttempt();
                return response;
            }

            TimeSpan durationBetweenLastFailedAttempt = DateTime.UtcNow - this.lastFailedLoginAttempt;

            // if one hour has passed since last failed attempt then reset attempt count
            TimeSpan durationBeforeLoginAttemptCountResets = TimeSpan.FromTicks(failedLoginAttemptDelay.Ticks * failedLoginAttemptsBeforeDelay);
            if (durationBetweenLastFailedAttempt > durationBeforeLoginAttemptCountResets)
            {
                this.failedLoginAttemptCount = 0;
            }

            // if the user has failed 10 times then they have to wait 5 minutes before trying again
            if (this.failedLoginAttemptCount > failedLoginAttemptsBeforeDelay && durationBetweenLastFailedAttempt < failedLoginAttemptDelay)
            {
                TimeSpan durationBeforeNextAllowedAttempt = failedLoginAttemptDelay - durationBetweenLastFailedAttempt;
                response.message = $"You have made {this.failedLoginAttemptCount} failed login attempts and must wait "
                    + $"{durationBeforeNextAllowedAttempt.Minutes} minutes {durationBeforeNextAllowedAttempt.Seconds} seconds "
                    + $"before being allowed another attempt.";
                return response;
            }

            if (Cryptography.ValidatePassword(attemptedPassword, this.passwordHash))
            {
                this.failedLoginAttemptCount = 0;
                response.isAuthenticated = true;
                response.id = this.id;
                return response;
            }

            response.message = "Password is incorrect.";
            FailedLoginAttempt();
            return response;
        }

        void FailedLoginAttempt()
        {
            this.lastFailedLoginAttempt = DateTime.UtcNow;
            this.failedLoginAttemptCount++;
        }
    }
}
