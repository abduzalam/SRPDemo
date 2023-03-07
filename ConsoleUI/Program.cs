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
        StandardMessages.WelcomeMessage();

        // Ask for user information
        Person user = PersonDataCapture.Capture();

        // Checks to be sure the first and last names are valid

        bool isUserValid = PersonValidator.Validate(user);

        if (isUserValid == false)
        {
            StandardMessages.EndApplication();
            return;
        }
        // Create a username for the person
        AccountGenerated.CreateAccount(user);
        StandardMessages.EndApplication();

    }
}