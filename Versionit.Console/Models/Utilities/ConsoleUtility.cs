using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versionit.Models
{
    internal class ConsoleUtility
    {
        public void WriteLine(string message, string lineBreak = null)
        {
            Console.WriteLine(message);

            if (lineBreak != null)
            {
                Console.WriteLine(lineBreak);
            }
        }

        public string ReadParameters(string message, params string[] allowedParameters)
        {
            Console.WriteLine("");
            Console.Write(message);

            var input = Console.ReadLine()
                               .Trim()
                               .ToLowerInvariant();

            if (allowedParameters.Any() && !allowedParameters.Contains(input))
            {
                Console.WriteLine("Unknown input...");
                input = ReadParameters(message, allowedParameters);
            }

            return input;
        }

        public string ReadLine()
        {
            return Console.ReadLine()
                               .Trim()
                               .ToLowerInvariant();
        }

        public void WriteError(string message)
        {
            Console.WriteLine("ERROR: " + message);
        }

        public bool Confirm()
        {
            var input = ReadParameters("Is this correct? (yes | no)", "yes", "no");

            if (input == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
