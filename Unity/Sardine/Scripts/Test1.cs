using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity;
using System;
using UnityEngine.UIElements;

namespace test
{

// interface Interface 
// {
//     void Printing();
// }

public class Test1 : MonoBehaviour
{
    public Dropdown dropdown1;
    public Text example;

    void Start()
    {
      // Main();
    }
    
    public void Change(int index)
        {
        // string userInput;
        // string userInput1;
        // float intVal;
        // int Points;
        // int WhiteMinnow = 2;
        // int AquariumA = 50;
        // int AquariumB = 70;

        // ConsoleKeyInfo confirm;

       // example.text = names[index];

        if(index == 0)
        {
            example.color = Color.red;
        }
        
        else if (index == 1)
        {
            Debug.Log("Hello!");
        }
        
        else 
        {
            example.color = Color.white;
        }
    
        
        // do
        // {
        //     Console.Write ( "Enter number of White Minnows: " );
        //     userInput = Console.ReadLine ();
        //     //intVal = Convert.ToInt32(userInput);
        //     bool result = Single.TryParse ( userInput, out intVal);
        //     if ( result )
        //     {
        //         int Val1 = Convert.ToInt32(intVal);
        //         Points = Val1 * WhiteMinnow;
                
        //         Console.WriteLine ( "Fish Points: " + Points );
        //     } else {
        //         Console.WriteLine ( "Please enter a number" );
        //         break;
        //     }
        //     bool check = false;
        //     do {
        //         Console.WriteLine ( "Type 'no' to change number of fish, otherwise type 'yes' to continue" );
        //         confirm = Console.ReadKey ( true );
        //         check = !( ( confirm.Key == ConsoleKey.Y ) || ( confirm.Key == ConsoleKey.N ) );
        //     } while ( check );
        //     switch ( confirm.Key )
        //     {
        //         case ConsoleKey.N:; 
        //         break;
                 
        //         case ConsoleKey.Y: Console.WriteLine ( "Continued:" ); 
        //         Console.WriteLine("Enter Aquarium type (A or B?): ");
        //         userInput1 = Console.ReadLine();
        //         string userinputcaps = userInput1.ToUpper();
        //         Console.WriteLine($"You entered {userinputcaps}");
        //         int Val1 = Convert.ToInt32(intVal);
        //         Points = Val1 * WhiteMinnow;
        //         if(userinputcaps == "A")
        //         {
        //             if(Points <= AquariumA)
        //             {
        //                 Console.WriteLine("Aquarium not overcrowded");
        //             }

        //             else
        //             {
        //                 Console.WriteLine("Overcrowded");
        //             }
        //         }
        //         else if(userinputcaps == "B")
        //         {
        //             if(Points <= AquariumB)
        //             {
        //                 Console.WriteLine("Aquarium not overcrowded");
        //             }

        //             else
        //             {
        //                 Console.WriteLine("Overcrowded");
        //             }
        //         }

        //         else 
        //         {
        //             Console.WriteLine("Aquarium not recognsied. Please start again.");
        //         }
        //         break;
        //     } 
        // } while ( confirm.Key != ConsoleKey.Y );
        } 
    
 }

}


