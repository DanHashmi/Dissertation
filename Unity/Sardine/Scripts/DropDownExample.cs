using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DropDownExample : MonoBehaviour
{
    List<string> names = new List<string>() {"Please Select Aquarium","ColdwaterA", "ColdwaterB", "TemperateA", "TemperateB", "TropicalA", "TropicalB"};
    public Dropdown dropdown;
    public Text SelectedName;
    public int intVal;
    [SerializeField]
    private InputField input;
    // string userInput;
    // string userInput1;
    // float intVal;
    // int Points;
    // int WhiteMinnow = 2;
    // int AquariumA = 50;
    // int AquariumB = 70;
        
    // ConsoleKeyInfo confirm;
    
    public void DropDown_IndexChanged(int index)
    {
        SelectedName.text = names[index];

        if(index == 0)
        {
            SelectedName.color = Color.red;
        }
        
        else if (index == 2)
        {
            Debug.Log("This is " + intVal);
        }
        
        else 
        {
            SelectedName.color = Color.white;
        }
    }

    
    void Start()
    {
        PopulateList();
    }
     
    void PopulateList()
    {
        // public string path = @"C:\Users\hashm\Downloads\newfishtank\Fish\Assets\Sardine\Scripts";
        // Get list from file
        
        dropdown.AddOptions(names);
    }
    public void GetInput(string guess)
    {
        Debug.Log("You Entered " + guess);
        intVal = Convert.ToInt32(guess);
        input.text = "";
    }
}
