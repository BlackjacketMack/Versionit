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

namespace Versionit
{
    class VersionitConsole
    {
        private const string COMMAND_SETUP = "setup";
        private const string COMMAND_GET = "get";
        private const string COMMAND_SCRIPT = "script";
        private const string COMMAND_HELP = "help";
        private const string COMMAND_EXIT = "exit";

        private IVersionRepository _versionRepository;
        private ConsoleUtility _logger;
        private SetupParameters _setupParameters;
        private IFileUtility _fileUtility;
        private IMessageUtility _sqlMessageUtility;

        static void Main(string[] args)
        {
            var prompt = new VersionitConsole(PromptDependencyFactory.Resolve<IVersionRepository>());

            prompt.Init();
        }

        public VersionitConsole(IVersionRepository versionRepository)
        {
            _versionRepository = versionRepository;
            _logger = new ConsoleUtility();
            _setupParameters = new SetupParameters();
            _fileUtility = new FileUtility();
            _sqlMessageUtility = new SqlMessageUtility();
        }

        public void Init()
        {
            welcome();

            config();

            listen();
        }

        private void welcome()
        {
            _logger.WriteLine("==================================================");
            _logger.WriteLine("Versionit");
            _logger.WriteLine("Welcome simple database version management.");
            _logger.WriteLine("Type 'help' to begin.");
            _logger.WriteLine("==================================================");
        }

        private void config()
        {
            _setupParameters.Directory = ConfigurationManager.AppSettings["Directory"];
        }

        private void listen()
        {
            var input = _logger.ReadLine();

            try
            {
                var parameters = parseInput(input);

                var command = createCommand(parameters);

                command.Run();

                _logger.WriteLine("--");
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex.Message);
            }

            listen();
        }

        private CommandParameters parseInput(string input)
        {
            var inputSplit = input.Split(' ').ToList();

            var command = new CommandParameters();
            command.Name = inputSplit[0].ToLower();

            var name = inputSplit[0];
            
            for (int i = 1; i < inputSplit.Count(); i++)
            {
                var key = inputSplit[i];

                var value = inputSplit.ElementAtOrDefault(i+1);

                if(value == null || value.StartsWith("--")){

                    command.Attributes.Add(inputSplit[i], null);
                }
                else
                {
                    command.Attributes.Add(key,value);
                    i++;
                }
            }

            return command;
        }

        private ICommand createCommand(CommandParameters commandParameters)
        {
            ICommand command = null;

            if (commandParameters.Name == COMMAND_SETUP)
            {
                command = new SetupCommand(commandParameters,_setupParameters);
            }
            else if (commandParameters.Name == COMMAND_GET)
            {
                command = new GetCommand(commandParameters, _setupParameters, _versionRepository);
            }
            else if (commandParameters.Name == COMMAND_HELP)
            {
                command = new HelpCommand(commandParameters,typeof(SetupCommand),
                                                            typeof(GetCommand),
                                                            typeof(ScriptCommand),
                                                            typeof(ExitCommand));
            }
            else if (commandParameters.Name == COMMAND_SCRIPT)
            {
                command = new ScriptCommand(commandParameters, _setupParameters, _versionRepository, _fileUtility, new SqlMessageUtility());
            }
            else if (commandParameters.Name == COMMAND_EXIT)
            {
                command = new ExitCommand(commandParameters);
            }

            return command;
        }
    }
}
