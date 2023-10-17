using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave1
{
    public class Employee
    {
        public string CreateEmployeeID(string sirname, string surname)
        {
            if (sirname.Length >= 4 && surname.Length >= 6)
            {
                return String.Format("{0}{1}{2}",
                sirname.Substring(0, 3),
                surname.Substring(0, 5),
                DateTime.Now.Millisecond).ToUpper();
            }
            if (surname.Length >= 6)
            {
                return String.Format("{0}{1}{2}",
               sirname.Substring(0, sirname.Length),
               surname.Substring(0, 5),
               DateTime.Now.Millisecond).ToUpper();
            }
            return String.Format("{0}{1}{2}{3}",
               sirname.Substring(0, sirname.Length),
               surname.Substring(0, surname.Length),
               GetRandomString(sirname.Length, surname.Length),
               DateTime.Now.Millisecond).ToUpper();
        }

        private string GetRandomString(int sirNameLength, int surNameLength)
        {
            Random rand = new Random();
            int stringLength = 8  - (sirNameLength + surNameLength);
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < stringLength; i++)
            {

                // Generating a random number. 
                randValue = rand.Next(26);

                // Generating random character by converting 
                // the random number into character. 
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string. 
                str = str + letter;
            }
            return str;
        }
    }
}
