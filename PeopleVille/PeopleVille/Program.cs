using Library;

Director director = new();
director.ResetDay();
director.ConstructItems();
director.ConstructPeople();
director.ConstructLocations();
director.FireEvent();
while (true)
{
    Console.WriteLine("Tab any to go to the next day");
    Console.ReadLine();
    director.NextDay();
    director.FireEvent();
}
