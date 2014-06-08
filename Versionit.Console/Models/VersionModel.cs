using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versionit.Models
{
    public class VersionModel
    {
        public string Database { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string UpScript { get; set; }
        public string DownScript { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
