using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveWordFiveLetters
{
    public class GetWords
    {
        public List<string> Words(string file)
        {
            try
            {
                String line;
                List<string> list = new List<string>();
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(file);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    list.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                return list;
            }
            catch
            {
                return new List<string>();
            };
        }
    }
}
