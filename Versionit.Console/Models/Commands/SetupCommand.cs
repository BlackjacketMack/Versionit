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
    [Description(@"setup        
[--dir <name>   ]  Sets the working directory. 
[--list         ] Outputs current setup.
*Setup reads from app.config when loading.")]
    class SetupCommand : ICommand
    {
        public const string COMMAND_SETUP_DIR = "--dir";
        public const string COMMAND_SETUP_LIST = "--list";

        private CommandParameters _commandParameters;

        private SetupParameters _setupParameters;

        private ConsoleUtility _utility;

        public SetupCommand(CommandParameters parameters, SetupParameters setupParameters)
        {
            _commandParameters = parameters;

            _setupParameters = setupParameters;

            _utility = new ConsoleUtility();
        }

        public void Run()
        {
            if (_commandParameters.Attributes.ContainsKey(COMMAND_SETUP_LIST))
            {
                _utility.WriteLine("Base Directory: " + _setupParameters.Directory);
                _utility.WriteLine("Working Directory: " + _setupParameters.WorkingDirectory);
            }

            if (_commandParameters.Attributes.ContainsKey(COMMAND_SETUP_DIR))
            {
                _setupParameters.Directory = _commandParameters.Attributes[COMMAND_SETUP_DIR];

                _utility.WriteLine("Working directory set to " + _setupParameters.Directory);
            }
        }

        private void saveSetup()
        {

        }
    }
}
