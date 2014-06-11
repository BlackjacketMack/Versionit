using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versionit.Core;
using Versionit.Data;
using Versionit.Models;
using System.ComponentModel;

namespace Versionit
{
    [Description(@"script   
[--auto <up,down>] Up is default if no value given.
[--from <name> --to <name>]")]
    class ScriptCommand : ICommand
    {
        public const string COMMAND_SCRIPT_AUTO = "--auto";
        public const string COMMAND_SCRIPT_FROM = "--from";
        public const string COMMAND_SCRIPT_TO = "--to";

        private CommandParameters _commandParameters;

        private ConsoleUtility _utility;

        private SetupParameters _setupParameters;

        private IVersionRepository _versionRepository;

        private IFileUtility _fileUtility;

        private IMessageUtility _messageUtility;

        private IEnumerable<VersionScript> _versionScripts;

        private VersionDirections _versionDirection;

        private string _from;
        private string _to;
        private bool _isAuto;
        private string _direction;

        public ScriptCommand(CommandParameters commandParameters, 
                             SetupParameters setupParameters, 
                             IVersionRepository versionRepository,
                            IFileUtility fileUtility,
                            IMessageUtility messageUtility)
        {
            _commandParameters = commandParameters;

            _setupParameters = setupParameters;

            _utility = new ConsoleUtility();

            _versionRepository = versionRepository;

            _fileUtility = fileUtility;

            _messageUtility = messageUtility;
        }

        public void Run()
        {
            parseParameters();

            _versionScripts = getVersionScripts();

            var headerMessage = getHeaderMessage();
            
            var script = new StringBuilder();

            script.AppendLine(_messageUtility.Header(headerMessage));

            foreach(var versionScript in _versionScripts)
            {
                script.AppendLine(_messageUtility.Comment("Script " + versionScript.Name));
                script.AppendLine(versionScript.Script);
            }

            var fileName = _fileUtility.WriteFile(script.ToString(), _setupParameters);

            start(fileName);
        }

        private string getHeaderMessage()
        {
            var headerMessage = "VERSIONIT - Script generated on " + DateTime.UtcNow.ToString();

            headerMessage += String.Format(
@"

Going from {0} to {1}.
The following versions have been concatenated:
{2}
",
_from,
_to,
String.Join(System.Environment.NewLine, _versionScripts.Select(s => s.Name)));

            return headerMessage;
        }

        private void parseParameters()
        {
            _isAuto = _commandParameters.Attributes.ContainsKey(COMMAND_SCRIPT_AUTO);
            _from = _commandParameters.GetAttribute(COMMAND_SCRIPT_FROM,required:false);
            _to = _commandParameters.GetAttribute(COMMAND_SCRIPT_TO,required:false);

            if (_isAuto)
            {
                _versionDirection = (VersionDirections)Enum.Parse(typeof(VersionDirections), (_commandParameters.Attributes[COMMAND_SCRIPT_AUTO] ?? "up"),true);
            }
            else
            {
                _versionDirection = String.Compare(_from, _to) <= 0 ? VersionDirections.Up : VersionDirections.Down;
            }
        }

        private IEnumerable<VersionScript> getVersionScripts()
        {
            var versions = getVersions();

            foreach (var version in versions)
            {
                yield return new VersionScript
                {
                    VersionDirection = _versionDirection,
                    Version = version
                };
            }
        }

        private IEnumerable<VersionModel> getVersions()
        {
            var parameters = new GetVersionsParameters
            {
                Path = _setupParameters.Directory
            };

            if (_versionDirection == VersionDirections.Up)
            {
                parameters.Min = _from;
                parameters.Max = _to;
            }
            else
            {
                parameters.Min = _to;
                parameters.Max = _from;
            }

            var versions = _versionRepository.Get(parameters).Skip(1);

            if (_isAuto)
            {
                versions = new[] { versions.Last() };
            }

            if (_versionDirection == VersionDirections.Up)
            {
                versions = versions.OrderBy(ob => ob.Name);
            }
            else
            {
                versions = versions.OrderByDescending(ob => ob.Name);
            }

            return versions;
        }

        private void start(string filePath)
        {
            Process.Start(filePath);
        }
    }
}
