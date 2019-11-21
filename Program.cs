using System;

namespace test
{
    class Program: TemperateTankResidents
    {
        static void Main()
        {
        string userInput;
        string userInput1;
        float intVal;
        int Points;
        ConsoleKeyInfo confirm;
        do
        {
            Console.Write ( "Enter number of White Minnows: " );
            userInput = Console.ReadLine ();
            //intVal = Convert.ToInt32(userInput);
            bool result = Single.TryParse ( userInput, out intVal);
            if ( result )
            {
                int Val1 = Convert.ToInt32(intVal);
                Points = Val1 * WhiteMinnow;
                
                Console.WriteLine ( "Fish Points: " + Points );
            } else {
                Console.WriteLine ( "Please enter a number" );
                break;
            }
            bool check = false;
            do {
                Console.WriteLine ( "Type 'no' to change number of fish, otherwise type 'yes' to continue" );
                confirm = Console.ReadKey ( true );
                check = !( ( confirm.Key == ConsoleKey.Y ) || ( confirm.Key == ConsoleKey.N ) );
            } while ( check );
            switch ( confirm.Key )
            {
                case ConsoleKey.N:; 
                break;
                 
                case ConsoleKey.Y: Console.WriteLine ( "Continued:" ); 
                Console.WriteLine("Enter Aquarium type (A or B?): ");
                userInput1 = Console.ReadLine();
                string userinputcaps = userInput1.ToUpper();
                Console.WriteLine($"You entered {userinputcaps}");
                int Val1 = Convert.ToInt32(intVal);
                Points = Val1 * WhiteMinnow;
                if(userinputcaps == "A")
                {
                    if(Points <= AquariumA)
                    {
                        Console.WriteLine("Aquarium not overcrowded");
                    }

                    else
                    {
                        Console.WriteLine("Overcrowded");
                    }
                }
                else if(userinputcaps == "B")
                {
                    if(Points <= AquariumB)
                    {
                        Console.WriteLine("Aquarium not overcrowded");
                    }

                    else
                    {
                        Console.WriteLine("Overcrowded");
                    }
                }

                else 
                {
                    Console.WriteLine("Aquarium not recognsied. Please start again.");
                }
                break;
            } 
        } while ( confirm.Key != ConsoleKey.Y );
        }   
    }
}
