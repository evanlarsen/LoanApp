using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Merchant.Membership
{
    public class SharedValidation
    {
        public async Task<CommonValidationResponse> IsValidEmailFormat(string email, IAccountService accountService)
        {
            string validationCategory = "email";
            var validationResponse = new CommonValidationResponse();
            if (string.IsNullOrWhiteSpace(email))
            {
                validationResponse.AddErrorMessages(validationCategory, "Email is a required field");
            }

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                validationResponse.AddErrorMessages(validationCategory, "Email is not of proper format");
            }

            if (await accountService.IsEmailAlreadyTaken(email))
            {
                validationResponse.AddErrorMessages(validationCategory, "Email is not unique.");
            }
            return validationResponse;
        }

        public CommonValidationResponse IsValidPassword(string password, string validationCategory)
        {
            int minimumPasswordLength = 6;
            var validationResponse = new CommonValidationResponse();
            if (string.IsNullOrWhiteSpace(password))
            {
                validationResponse.AddErrorMessages(validationCategory, "Password is a required field.");
            }
            if (password.Length <= minimumPasswordLength)
            {
                validationResponse.AddErrorMessages(validationCategory, $"Password has to be atleast {minimumPasswordLength} characters.");
            }
            return validationResponse;
        }
    }
}
