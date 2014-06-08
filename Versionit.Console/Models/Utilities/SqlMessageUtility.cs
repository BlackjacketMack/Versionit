using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versionit.Models
{
    internal class SqlMessageUtility : IMessageUtility
    {
        public string Header(string message)
        {
            return String.Format(
@"/*******************************************************
{0}
*******************************************************/", message);
        }

        public string Comment(string message)
        {
            return String.Format(
@"/*
{0}
*/", message);
        }
    }
}
