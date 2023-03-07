using ConsoleUI;

internal class Program
{
    /// <summary>
    /// SRP : There is only one reason to change the class which is already designed
    /// OR the class should only have one resposibilty 
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        Console.WriteLine("Weclome to my application!");

        // Ask for user information
        Person user = new Person();

        Console.Write("What is your first name: ");
        user.FirstName = Console.ReadLine();

        Console.Write("What is your last name: ");
        user.LastName = Console.ReadLine();

        // Checks to be sure the first and last names are valid

        if(string.IsNullOrWhiteSpace(user.FirstName))
        {
            Console.WriteLine("Yoou did not give us a valid first name!");
            Console.ReadLine();
            return;
        }

        if (string.IsNullOrWhiteSpace(user.LastName))
        {
            Console.WriteLine("Yoou did not give us a valid last name!");
            Console.ReadLine();
            return;
        }

        // Create a username for the person
        Console.WriteLine($"Your username is {user.FirstName.Substring(0,1) }{user.LastName} ");
        Console.ReadLine();

    }
}