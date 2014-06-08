using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versionit.Core;
using Versionit.Data;
using Versionit.Models;

namespace Versionit
{
    class HelpCommand : ICommand
    {
        private CommandParameters _parameters;

        private ConsoleUtility _utility;

        private Type[] _types;

        public HelpCommand(CommandParameters parameters,params Type[] types)
        {
            _parameters = parameters;

            _utility = new ConsoleUtility();

            _types = types;
        }

        public void Run()
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
