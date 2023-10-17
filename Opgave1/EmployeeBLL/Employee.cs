namespace EmployeeBLL
{

    public class Employee
    {
        public EmployeeData CreateEmployeeID(string sirName, string surName)
        {
            EmployeeData employeeData;
            string id;
            if (sirName.Length >= 4 && surName.Length >= 6)
            {
                id = String.Format("{0}{1}{2}",
                sirName.Substring(0, 3),
                surName.Substring(0, 5),
                DateTime.Now.Millisecond).ToUpper();
                return new EmployeeData(id, id + "@fiktivefirma.dk");
            }
            if (surName.Length >= 6)
            {
                id = string.Format("{0}{1}{2}",
               sirName.Substring(0, sirName.Length),
               surName.Substring(0, 5),
               DateTime.Now.Millisecond).ToUpper();
                return new EmployeeData(id, id + "@fiktivefirma.dk");
            }
            id = String.Format("{0}{1}{2}{3}",
               sirName.Substring(0, sirName.Length),
               surName.Substring(0, surName.Length),
               GetRandomString(sirName.Length, surName.Length),
               DateTime.Now.Millisecond).ToUpper();
            return new EmployeeData(id, id + "@fiktivefirma.dk");


        }
        private string GetRandomString(int sirNameLength, int surNameLength)
        {
            Random rand = new Random();
            int stringLength = 8 - (sirNameLength + surNameLength);
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