﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versionit.Models
{
    internal interface IFileUtility
    {
        string WriteFile(string text, SetupParameters setupParameters);
    }
}
