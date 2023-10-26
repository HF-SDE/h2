using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FiveWordFiveLettersWPF.libs
{
    internal class Utils
    {
        /// <summary>
        /// Checks if a given file path is a valid path to a text file with a '.txt' extension.
        /// </summary>
        /// <param name="filePath">The file path to be validated.</param>
        /// <returns>
        ///   <c>true</c> if the file path is valid and represents a '.txt' text file; otherwise, <c>false</c>.
        /// </returns>
        internal bool IsValidFilePath(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                string pattern = @"^.+\.txt$";
                return Regex.IsMatch(filePath, pattern, RegexOptions.IgnoreCase);
            }
            return false;
        }
    }
}
