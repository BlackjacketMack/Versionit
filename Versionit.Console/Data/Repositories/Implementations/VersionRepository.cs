﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versionit.Models;

namespace Versionit.Data
{
    internal class VersionRepository : IVersionRepository
    {
        public const string DOWNSCRIPT = "Down.sql";
        public const string UPSCRIPT = "Up.sql";
        private StringComparison _stringComparison = StringComparison.OrdinalIgnoreCase;

        public IEnumerable<VersionModel> Get(GetVersionsParameters parameters)
        {
            var directory = new DirectoryInfo(parameters.Path);

            if (!directory.Exists)
            {
                throw new ApplicationException("Specified directory does not exist.");
            }

            var subDirectories = directory.GetDirectories().Where(w=>!w.Name.StartsWith("_"));

            subDirectories = subDirectories.Where(w =>
                                    (parameters.Single == null || String.Compare(w.Name, parameters.Single, _stringComparison) == 0) &&
                                    (parameters.Min == null || String.Compare(w.Name, parameters.Min, _stringComparison) >= 0) &&
                                    (parameters.Max == null || String.Compare(w.Name, parameters.Max, _stringComparison) <= 0))
                                    .OrderBy(ob => ob.Name);

            foreach (var subDirectory in subDirectories)
            {
                yield return new VersionModel
                {
                    Database = directory.Name,
                    ID = 0,
                    Name = subDirectory.Name,
                    DownScript = readScript(subDirectory.FullName + "\\" + DOWNSCRIPT),
                    UpScript = readScript(subDirectory.FullName + "\\" + UPSCRIPT)
                };
            }
        }

        private string readScript(string filePath)
        {
            var fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                return null;
            }

            var script = (String)null;

            using (var stream = new StreamReader(filePath))
            {
                script = stream.ReadToEnd();
            }

            return script;
        }
    }
}
