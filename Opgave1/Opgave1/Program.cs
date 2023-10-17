using Opgave1;

Console.WriteLine("Lav nyt medarbeder ID.");
Console.Write("Fornavn: ");
var sirname = Console.ReadLine();

//Efternavn
Console.Write("Efternavn: ");
var surname = Console.ReadLine();
var employee = new Employee();

Console.WriteLine("MedarbejderID: {0}", employee.CreateEmployeeID(sirname, surname));
