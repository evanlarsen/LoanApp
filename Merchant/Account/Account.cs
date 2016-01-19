using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Merchant.Account
{
    public class Account
    {
        public readonly Guid Id;
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime LastFailedLoginAttempt { get; private set; }
        public int FailedLoginAttemptCount { get; private set; }

        public const int FailedLoginAttemptsBeforeDelay = 10;
        const int MaximumFailedLoginAttemptsBeforeAccountLocked = 1000;
        readonly TimeSpan FailedLoginAttemptDelay = TimeSpan.FromMinutes(5);
        public const int MinimumPasswordLength = 6;

        public Account(Guid id)
        {
            this.Id = id;
        }

        public Account(Guid id, string email, string passwordHash, string phoneNumber)
            : this(id)
        {
            this.Email = email;
            this.PasswordHash = passwordHash;
            this.PhoneNumber = phoneNumber;
        }

        public async Task Register(string email, string password, string phoneNumber, IAccountService accountService)
        {
            await IsValidEmailFormat(email, accountService);
            IsValidPassword(password);
            IsValidPhoneNumber(phoneNumber);

            this.Email = email;
            this.PasswordHash = Cryptography.CreateHash(password);
            this.PhoneNumber = phoneNumber;
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (!Cryptography.ValidatePassword(oldPassword, this.PasswordHash))
            {
                throw new ValidationException("The old password provided was not correct");
            }
            this.PasswordHash = Cryptography.CreateHash(newPassword);
        }

        public LoginResponse Login(string attemptedPassword)
        {
            var response = new LoginResponse() { IsAuthenticated = false };
            TimeSpan durationBetweenLastFailedAttempt = DateTime.UtcNow - this.LastFailedLoginAttempt;
            
            // if one hour has passed since last failed attempt then reset attempt count
            if (durationBetweenLastFailedAttempt > FailedLoginAttemptDelay)
            {
                this.FailedLoginAttemptCount = 0;
            }

            if (this.FailedLoginAttemptCount > MaximumFailedLoginAttemptsBeforeAccountLocked)
            {
                response.Message = "Account has been locked";
                FailedLoginAttempt();
                return response;
            }
            if (this.FailedLoginAttemptCount > FailedLoginAttemptsBeforeDelay && durationBetweenLastFailedAttempt < FailedLoginAttemptDelay)
            {
                TimeSpan durationBeforeNextAllowedAttempt = FailedLoginAttemptDelay - durationBetweenLastFailedAttempt;
                response.Message = $"You have made {this.FailedLoginAttemptCount} failed login attempts and must wait "
                    + $"{durationBeforeNextAllowedAttempt.Minutes} minutes {durationBeforeNextAllowedAttempt.Seconds} seconds "
                    + $"before being allowed another attempt.";
                FailedLoginAttempt();
                return response;
            }

            if (Cryptography.ValidatePassword(attemptedPassword, this.PasswordHash))
            {
                this.FailedLoginAttemptCount = 0;
                response.IsAuthenticated = true;
                return response;
            }

            response.Message = "Password is incorrect.";
            FailedLoginAttempt();
            return response;
        }

        private void FailedLoginAttempt()
        {
            this.LastFailedLoginAttempt = DateTime.UtcNow;
            this.FailedLoginAttemptCount++;
        }

        async Task IsValidEmailFormat(string email, IAccountService accountService)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ValidationException("Email is a required field");
            }

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                throw new ValidationException("Email is not of proper format");
            }

            if (!await accountService.IsEmailUnique(email))
            {
                throw new ValidationException("Email is not unique.");
            }
        }

        void IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ValidationException("Password is a required field.");
            }
            if (password.Length <= MinimumPasswordLength)
            {
                throw new ValidationException($"Password has to be atleast {MinimumPasswordLength} characters.");
            }

        }

        void IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ValidationException("Phone Number is a required field.");
            }
        }
    }
}
