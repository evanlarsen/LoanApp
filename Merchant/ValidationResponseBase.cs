using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant
{
    public abstract class ValidationResponseBase
    {
        public Dictionary<string, List<string>> errorMessages;
        public bool anyErrorMessages
        {
            get
            {
                return this.errorMessages.Count > 0;
            }
        }

        public ValidationResponseBase()
        {
            this.errorMessages = new Dictionary<string, List<string>>();
        }
    }
}
