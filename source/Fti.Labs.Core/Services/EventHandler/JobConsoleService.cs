using Fti.Labs.Core.Interfaces.Data;
using Relativity.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fti.Labs.Core.Services.EventHandler
{
    public class JobConsoleService
    {
        private IJobRepository jobRepository;

        public JobConsoleService(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        public void Run(int workspaceId, int artifactId)
        {
            this.jobRepository.UpdateStatus(workspaceId, artifactId, 1);
        }

        public void Cancel(int workspaceId, int artifactId)
        {
            this.jobRepository.UpdateStatus(workspaceId, artifactId, 5);
        }
    }
}
