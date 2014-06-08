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
    class HelpCommand : ICommand
    {
        private CommandParameters _parameters;

        private ConsoleUtility _utility;

        public HelpCommand(CommandParameters parameters)
        {
            _parameters = parameters;

            _utility = new ConsoleUtility();
        }

        public void Run()
        {
            _utility.WriteLine("setup --dir [directory to database]");
            _utility.WriteLine("get --version [number of version]");
            _utility.WriteLine("help");
            _utility.WriteLine("exit");
        }
    }
}
