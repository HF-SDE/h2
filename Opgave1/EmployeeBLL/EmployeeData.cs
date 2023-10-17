namespace EmployeeBLL
{
    public class EmployeeData
    {
        public string Id { get; set; }
        public string Mail { get; set; }
        public EmployeeData(string id, string mail)
        {
            Id = id;
            Mail = mail;
        }
    }
}