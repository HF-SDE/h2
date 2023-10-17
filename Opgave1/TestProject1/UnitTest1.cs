using EmployeeBLL;

namespace TestProject1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("bobander", "Bob", "Andersen")]
        [InlineData("CAHANSEN", "Carl", "Hansen")]
        [InlineData("BOIBXXXX", "Bo", "Ib")]
        public void Test1(string expectedResult, string sirname, string surname)
        {
            var employee = new Employee();

            //art
            var employeeID = employee.CreateEmployeeID(sirname, surname);

            //assert
            Assert.Equal(expectedResult, employeeID.Substring(0, 8));
        }
    }
}