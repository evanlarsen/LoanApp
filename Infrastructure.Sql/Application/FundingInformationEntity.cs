using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Application
{
    public class FundingInformationEntity
    {
        public string DesiredAmount { get; set; }
        public string MinimumAmount { get; set; }
        public string AverageMonthlyTotalSales { get; set; }
        public string AverageCreditCardSales { get; set; }
        public string ProposedUseForFunds { get; set; }
        public string ProcessingCompany { get; set; }
        public bool? HasApplicantEverBeenInBankruptcy { get; set; }
        public bool? HasApplicantOpenCashAdvances { get; set; }
        public bool? AnySuitsJudgementsLiens { get; set; }
    }
}
