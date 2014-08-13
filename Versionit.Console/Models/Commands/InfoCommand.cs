using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versionit.Core;
using Versionit.Data;
using Versionit.Models;
using Commandit;

namespace Versionit
{
    class InfoCommand : ICommand
    {
        public string Name
        {
            get { return "Info"; }
        }


        private ConsoleUtility _utility;

        private Type[] _types;

        public InfoCommand(params Type[] types)
        {

            _utility = new ConsoleUtility();

            _types = types;
        }

        public void Run(ICommandContext context)
        {
            foreach (var type in _types)
            {
                var descriptionAttribute = type.GetCustomAttributes(typeof(DescriptionAttribute),false).Cast<DescriptionAttribute>().SingleOrDefault();
                
                if (descriptionAttribute != null) 
                {
                    _utility.WriteLine(descriptionAttribute.Description);
                    _utility.WriteLine("");
                }
            }
        }
    }
}
