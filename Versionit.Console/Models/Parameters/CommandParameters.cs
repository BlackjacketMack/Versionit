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
    class CommandParameters
    {
        public string Name { get; set; }

        public IDictionary<string,string> Attributes { get; set; }

        public CommandParameters()
        {
            this.Attributes = new Dictionary<string, string>();
        }
    }
}
