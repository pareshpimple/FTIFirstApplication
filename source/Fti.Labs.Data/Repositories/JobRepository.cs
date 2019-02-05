using Fti.Labs.Core.Constants;
using Fti.Labs.Core.Interfaces.Data;
using kCura.Relativity.Client;
using Relativity.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fti.Labs.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IHelper helper;

        public JobRepository(IHelper helper)
        {
            this.helper = helper;
        }

        public void UpdateStatus(int workspaceId, int artifactId, int status)
        {
            using (var rsapiClient = this.helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.CurrentUser))
            {
                rsapiClient.APIOptions.WorkspaceID = workspaceId;

                var job = rsapiClient.Repositories.RDO.ReadSingle(artifactId);
                var field = job.Fields.First(f => f.Guids.Any(g => g == Guids.Fields.Job.Status));
                field.Value = status;
                rsapiClient.Repositories.RDO.Update(job);
            }
        }
    }
}
