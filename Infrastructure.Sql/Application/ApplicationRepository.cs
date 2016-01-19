using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Infrastructure.Sql.Application
{
    public class ApplicationRepository
    {
        readonly ApplicationContext context;

        public ApplicationRepository(string connectionString)
        {
            this.context = new ApplicationContext(connectionString);
        }

        public async Task Save(ApplicationEntity application)
        {
            var entity = await this.context.Applications.SingleOrDefaultAsync(a => a.Id.Equals(application.Id));

            if (entity == null)
            {
                entity = new ApplicationEntity();
                this.context.Applications.Add(entity);
            }
            else
            {
                this.context.Applications.Attach(application);
                this.context.Entry(application).State = EntityState.Modified;
            }
            await this.context.SaveChangesAsync();
        }
    }
}
