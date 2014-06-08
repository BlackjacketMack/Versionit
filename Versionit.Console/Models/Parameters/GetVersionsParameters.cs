using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Versionit.Models
{
    class GetVersionsParameters
    {
        public string Path { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Single { get; set; }
    }
}
