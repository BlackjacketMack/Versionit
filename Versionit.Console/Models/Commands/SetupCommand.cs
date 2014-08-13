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
    [Description(@"setup        
[--dir <name>   ]  Sets the working directory. 
[--list         ] Outputs current setup.
*Setup reads from app.config when loading.")]
    class SetupCommand : ICommand
    {
        public string Name
        {
            get { return "Setup"; }
        }

        public const string COMMAND_SETUP_DIR = "--dir";
        public const string COMMAND_SETUP_LIST = "--list";

        private SetupParameters _setupParameters;

        private ConsoleUtility _utility;

        public SetupCommand(SetupParameters setupParameters)
        {
            _setupParameters = setupParameters;

            _utility = new ConsoleUtility();
        }

        public void Run(ICommandContext context)
        {
            var parameters = context.Parameters;

            if (parameters.Attributes.ContainsKey(COMMAND_SETUP_LIST))
            {
                _utility.WriteLine("Base Directory: " + _setupParameters.Directory);
                _utility.WriteLine("Working Directory: " + _setupParameters.WorkingDirectory);
            }

            if (parameters.Attributes.ContainsKey(COMMAND_SETUP_DIR))
            {
                _setupParameters.Directory = parameters.Attributes[COMMAND_SETUP_DIR];

                _utility.WriteLine("Working directory set to " + _setupParameters.Directory);
            }
        }

        private void saveSetup()
        {

        }
    }
}
