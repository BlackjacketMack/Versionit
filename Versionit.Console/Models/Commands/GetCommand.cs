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
    class GetCommand : ICommand
    {
        public const string COMMAND_GET_ALL = "--all";
        public const string COMMAND_GET_SINGLE = "--single";
        public const string COMMAND_GET_MIN = "--min";
        public const string COMMAND_GET_MAX = "--max";

        private CommandParameters _commandParameters;

        private SetupParameters _setupParameters;

        private ConsoleUtility _utility;

        private IVersionRepository _versionRepository;

        public GetCommand(CommandParameters commandParameters,
                        SetupParameters setupParameters,
                        IVersionRepository versionRepository)
        {
            _commandParameters = commandParameters;

            _setupParameters = setupParameters;

            _utility = new ConsoleUtility();

            _versionRepository = versionRepository;
        }

        public void Run()
        {
            if (_setupParameters.Directory == null)
            {
                throw new ApplicationException("No working directory set.  Use 'setup --dir [workingdirectory] to continue.");
            }

            var versions = _versionRepository.Get(new GetVersionsParameters { 
                                Path = _setupParameters.Directory,
                                Single = _commandParameters.Attributes.ContainsKey(COMMAND_GET_SINGLE) ? _commandParameters.Attributes[COMMAND_GET_SINGLE] : null,
                                Min = _commandParameters.Attributes.ContainsKey(COMMAND_GET_MIN) ? _commandParameters.Attributes[COMMAND_GET_MIN] : null,
                                Max = _commandParameters.Attributes.ContainsKey(COMMAND_GET_MAX) ? _commandParameters.Attributes[COMMAND_GET_MAX] : null
            });

            foreach (var version in versions)
            {
                _utility.WriteLine(String.Format(   "Version # {0} \t Down Script: {1} \t Up Script: {2}", 
                                                version.Name,
                                                version.DownScript != null,
                                                version.UpScript != null));
            }
        }
    }
}
