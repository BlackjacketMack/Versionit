using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versionit.Core;
using Versionit.Data;
using Versionit.Models;
using Commandit;

namespace Versionit
{
    class ExitCommand : ICommand
    {
        public string Name
        {
            get { return "Exit"; }
        }

        private ConsoleUtility _utility;

        public ExitCommand()
        {
            _utility = new ConsoleUtility();
        }

        public void Run(ICommandContext context)
        {
            _utility.WriteLine("Success! Press any key to continue.");
            Console.ReadKey();
        }
    }
}
