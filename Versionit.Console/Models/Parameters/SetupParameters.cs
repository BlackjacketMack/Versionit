using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versionit.Core;
using Versionit.Data;
using Versionit.Models;

namespace Versionit
{
    class SetupParameters
    {
        public string Directory { get; set; }
        public string WorkingDirectory
        {
            get
            {
                return this.Directory + "\\_versionit";
            }
        }
    }
}
