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
        public const StringComparison ComparisonType = StringComparison.OrdinalIgnoreCase;

        public string Name { get; set; }

        public IDictionary<string,string> Attributes { get; set; }

        public CommandParameters()
        {
            this.Attributes = new Dictionary<string, string>();
        }

        public string GetAttribute(string key, bool required = true)
        {
            var containsKey = this.Attributes.ContainsKey(key);

            if (required && !containsKey)
            {
                throw new ArgumentException("Attribute missing", key);
            }
            else if (!containsKey)
            {
                return null;
            } 

            return this.Attributes[key];
        }
    }
}
