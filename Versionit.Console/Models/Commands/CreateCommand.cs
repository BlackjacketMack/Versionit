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
using Commandit;

namespace Versionit
{
    [Description(@"create   
[--n <versionname>]")]
    class CreateCommand : ICommand
    {
        private const string COMMAND_CREATE_NAME = "name";

        public string Name
        {
            get { return "Create"; }
        }

        private ICommandContext _context;

        private SetupParameters _setupParameters;

        private IVersionRepository _versionRepository;

        private string _versionName;

        public CreateCommand(SetupParameters setupParameters, 
                             IVersionRepository versionRepository)
        {
            _setupParameters = setupParameters;
            _versionRepository = versionRepository;
        }

        public void Run(ICommandContext context)
        {
            _context = context;

            parseParameters();

            var versionModel = new VersionModel
            {
                Database = _setupParameters.Directory,
                Name = _versionName
            };

            _versionRepository.Create(versionModel);
        }

        private void parseParameters()
        {
            var parameters = _context.Parameters;

            _versionName = parameters.GetAttribute(COMMAND_CREATE_NAME,required:true);
        }
    }
}
