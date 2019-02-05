using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fti.Labs.Core.Interfaces.Data
{
    public interface IJobRepository
    {
        void UpdateStatus(int workspaceId, int artifactId, int status);
    }
}
