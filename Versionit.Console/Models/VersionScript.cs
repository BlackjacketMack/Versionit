using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versionit.Models
{
    public class VersionScript
    {
        public VersionDirections VersionDirection { get; set; }

        public VersionModel Version { get; set; }

        public string Name
        {
            get
            {
                return this.Version.Name + " " + this.VersionDirection.ToString();
            }
        }

        public string Script
        {
            get
            {
                return this.VersionDirection == VersionDirections.Down ? this.Version.DownScript : this.Version.UpScript;
            }
        }
    }
}
