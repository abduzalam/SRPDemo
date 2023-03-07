# SRPDemo

SRP Says There is only one reason to change the class which is already designed OR the class should only have one resposibility 

Take the following class
```
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
```

Lets say if we want to change the welcome message or the way to check the username to be changed or the way to generate the username to be modified, we have to modify the class. So lets refactor this class as shown below.

first add a new class say StandardMessages and modify the initial class .
if you think to move those messages to two different classes, that also fine, its up to you 
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class StandardMessages
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine("Weclome to my application!");
        }

        public static void EndApplication()
        {
            Console.ReadLine();
        }
    }
}

```
The main code after modification
```
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
        Person user = new Person();

        Console.Write("What is your first name: ");
        user.FirstName = Console.ReadLine();

        Console.Write("What is your last name: ");
        user.LastName = Console.ReadLine();

        // Checks to be sure the first and last names are valid

        if(string.IsNullOrWhiteSpace(user.FirstName))
        {
            Console.WriteLine("Yoou did not give us a valid first name!");
            StandardMessages.EndApplication();
            return;
        }

        if (string.IsNullOrWhiteSpace(user.LastName))
        {
            Console.WriteLine("Yoou did not give us a valid last name!");
            StandardMessages.EndApplication();
            return;
        }

        // Create a username for the person
        Console.WriteLine($"Your username is {user.FirstName.Substring(0,1) }{user.LastName} ");
        StandardMessages.EndApplication();

    }
}
```
Next we can move the Person data collection to a different class

Added a new class
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class PersonDataCapture
    {
        public static Person Capture()
        {
            Person user = new Person();

            Console.Write("What is your first name: ");
            user.FirstName = Console.ReadLine();

            Console.Write("What is your last name: ");
            user.LastName = Console.ReadLine();

            return user;
        }
    }
}

```
so with the new class, if we want change the first name to given name, or surname , or to capture email address some other, we can modify the PersonDataCapture class alone

then modify the code of the main program
```
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

        if (string.IsNullOrWhiteSpace(user.FirstName))
        {
            Console.WriteLine("Yoou did not give us a valid first name!");
            StandardMessages.EndApplication();
            return;
        }

        if (string.IsNullOrWhiteSpace(user.LastName))
        {
            Console.WriteLine("Yoou did not give us a valid last name!");
            StandardMessages.EndApplication();
            return;
        }

        // Create a username for the person
        Console.WriteLine($"Your username is {user.FirstName.Substring(0,1) }{user.LastName} ");
        StandardMessages.EndApplication();

    }
}
```

Next is validator, we can extract validator code and move to another classes

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class PersonValidator
    {
        public static bool Validate(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                StandardMessages.DisplayValidationError("first name");
                return false;
            }

            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                StandardMessages.DisplayValidationError("last name");
                return false;
            }
            return true;
        }
    }
}
```
Modified StandardMessages class to include new DisplayValidationError method
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class StandardMessages
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine("Weclome to my application!");
        }

        public static void EndApplication()
        {
            Console.ReadLine();
        }

        public static void DisplayValidationError(string fieldName)
        {
            Console.WriteLine($"You did not give us a valid {fieldName}");
        }
    }
}

```
Next added a new class for user name creation
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class AccountGenerated
    {
        public static void CreateAccount(Person user)
        {
            Console.WriteLine($"Your username is {user.FirstName.Substring(0, 1)}{user.LastName} ");
        }
    }
}

```
Final Main program
```
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
```
