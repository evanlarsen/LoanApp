using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant
{
    public class CommonValidationResponse : ValidationResponseBase
    {
        public void AddErrorMessages(string category, params string[] messages)
        {
            if (!this.errorMessages.ContainsKey(category))
            {
                this.errorMessages.Add(category, new List<string>());
            }
            this.errorMessages[category].AddRange(messages);
        }

        public void JoinValidationResponses(params ValidationResponseBase[] validationResponses)
        {
            foreach(var validationResponse in validationResponses)
            {
                if (validationResponse.anyErrorMessages)
                {
                    foreach(KeyValuePair<string, List<string>> errorMessages in validationResponse.errorMessages)
                    {
                        AddErrorMessages(errorMessages.Key, errorMessages.Value.ToArray());
                    }
                }
            }
        }

        public static CommonValidationResponse Ok()
        {
            return new CommonValidationResponse();
        }
    }
}
