using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versionit.Core;
using Versionit.Data;
using Versionit.Models;
using Commandit;

namespace Versionit
{
    class VersionitConsole : CommandConsole
    {
        private const string COMMAND_SETUP = "setup";
        private const string COMMAND_GET = "get";
        private const string COMMAND_SCRIPT = "script";
        private const string COMMAND_HELP = "help";
        private const string COMMAND_EXIT = "exit";

        private string[] _args; //initial args
        private IVersionRepository _versionRepository;
        private ConsoleUtility _logger;
        private SetupParameters _setupParameters;
        private IFileUtility _fileUtility;
        private IMessageUtility _sqlMessageUtility;

        static void Main(string[] args)
        {
            var prompt = new VersionitConsole(args,PromptDependencyFactory.Resolve<IVersionRepository>());

            prompt.Init();
        }

        public VersionitConsole(string[] args,IVersionRepository versionRepository) : base("Versionit")
        {
            _args = args;
            _versionRepository = versionRepository;
            _logger = new ConsoleUtility();
            _setupParameters = new SetupParameters();
            _fileUtility = new FileUtility();
            _sqlMessageUtility = new SqlMessageUtility();
        }

        public override void Init()
        {
            config();

            this.Commands = new ICommand[]{
                new SetupCommand(_setupParameters),
                new GetCommand(_setupParameters, _versionRepository),
                new InfoCommand(typeof(SetupCommand),
                                                            typeof(GetCommand),
                                                            typeof(ScriptCommand),
                                                            typeof(ExitCommand)),
                 new ScriptCommand(_setupParameters, _versionRepository, _fileUtility, new SqlMessageUtility()),
                 new CreateCommand(_setupParameters, _versionRepository),
                 new ExitCommand()
            };

            base.Init();
        }


        private void config()
        {
            var directory = (String)null;

            if (_args.Any())
            {
                var attributes = ParameterUtility.ParseAttributes(String.Join(" ",_args));

                if(attributes.ContainsKey("--dir")){
                    directory = attributes["--dir"];
                }
            }

            _setupParameters.Directory = directory;
        }
    }
}
