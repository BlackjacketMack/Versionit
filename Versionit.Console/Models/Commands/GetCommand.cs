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
    [Description(@"get   
[--all]                         Gets all
[--single <name>]               Gets one
[--min <name> --max <name>]     Gets between the min and max inclusive")]  
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
            var allAttributeNames = new[]{
                COMMAND_GET_ALL,
                COMMAND_GET_SINGLE,
                COMMAND_GET_MIN,
                COMMAND_GET_MAX
            };

            if (_setupParameters.Directory == null)
            {
                throw new ApplicationException("No working directory set.  Use 'setup --dir [workingdirectory] to continue.");
            }
            else if (!_commandParameters.Attributes.Select(s => s.Key).Intersect(allAttributeNames).Any())
            {
                throw new ApplicationException("Command parameters required.");
            }

            var versions = _versionRepository.Get(new GetVersionsParameters { 
                                Path = _setupParameters.Directory,
                                Single = _commandParameters.GetAttribute(COMMAND_GET_SINGLE,required:false),
                                Min = _commandParameters.GetAttribute(COMMAND_GET_MIN, required: false),
                                Max = _commandParameters.GetAttribute(COMMAND_GET_MAX, required: false)
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
