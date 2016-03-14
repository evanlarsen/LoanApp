using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Infrastructure.Sql.Application
{
    public class ApplicationService
    {
        readonly ApplicationContext context;

        public ApplicationService(ApplicationContext context)
        {
            this.context = context;
        }

        public Task<ApplicationEntity> Get(Guid id)
        {
            return this.context.Applications.SingleOrDefaultAsync(a => a.Id.Equals(id));
        }

        public async void UpdateBusinessInformation(BusinessInformationEntity businessInformation, Guid id)
        {
            var app = await Get(id);
            app.BusinessInformation = businessInformation;
            await this.context.SaveChangesAsync();
        }
    }
}
