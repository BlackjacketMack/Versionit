using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versionit.Core;
using Versionit.Data;
using Versionit.Models;

namespace Versionit
{
    class ScriptCommand : ICommand
    {
        public const string COMMAND_SCRIPT_SINGLE = "--single";

        private CommandParameters _commandParameters;

        private ConsoleUtility _utility;

        private SetupParameters _setupParameters;

        public ScriptCommand(CommandParameters commandParameters, 
                             SetupParameters setupParameters, 
                             IVersionRepository versionRepository)
        {
            _commandParameters = commandParameters;

            _setupParameters = setupParameters;

            _utility = new ConsoleUtility();
        }

        public void Run()
        {
            start("c:\\temp\\MyDatabase\\_scripts\\somefile.txt");
        }



        private void start(string filePath)
        {
            Process.Start(filePath);
        }
    }
}
