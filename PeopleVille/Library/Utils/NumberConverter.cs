namespace Library.Utils
{
    internal class NumberConverter
    {
        internal string ConvertWithLeadingZero(int number)
        {
            if (number < 10)
            {
                return "0" + number.ToString();
            }
            else
            {
                return number.ToString();
            }
        }
    }
}
