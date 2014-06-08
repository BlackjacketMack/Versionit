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
            createWorkingDirectory(setupParameters.WorkingDirectory);

            var fileName = setupParameters.WorkingDirectory + "\\" + DateTime.UtcNow.ToString("yyyyMMddTHHmmss") + ".sql";

            File.WriteAllText(fileName,text);

            return fileName;
        }

        private void createWorkingDirectory(string workingDirectory)
        {
            var directoryInfo = new DirectoryInfo(workingDirectory);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
    }
}
