using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versionit.Models;

namespace Versionit.Data
{
    internal interface IVersionRepository
    {
        IEnumerable<VersionModel> Get(GetVersionsParameters parameters);
        void Create(VersionModel model);
    }    
}
