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
    class ExitCommand : ICommand
    {
        private CommandParameters _parameters;

        private ConsoleUtility _utility;

        public ExitCommand(CommandParameters parameters)
        {
            _parameters = parameters;

            _utility = new ConsoleUtility();
        }

        public void Run()
        {

            _utility.WriteLine("Success! Press any key to continue.");
            Console.ReadKey();
        }
    }
}
