using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versionit.Models
{
    internal class FileUtility : IFileUtility
    {
        public string WriteFile(string text, SetupParameters setupParameters)
        {
            createDirectory(setupParameters.WorkingDirectory);
            createDirectory(setupParameters.ScriptDirectory);

             var fileName = setupParameters.ScriptDirectory + "\\" + DateTime.UtcNow.ToString("yyyyMMddTHHmmss") + ".sql";

            File.WriteAllText(fileName,text);

            return fileName;
        }

        private void createDirectory(string directory)
        { 
            var directoryInfo = new DirectoryInfo(directory);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
    }
}
