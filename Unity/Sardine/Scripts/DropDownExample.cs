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
    public void DropDown_IndexChanged(int index)
    {
        SelectedName.text = names[index];

        if(index == 0)
        {
            SelectedName.color = Color.red;
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
    
    void Update()
    {
        if(dropdown.value == 2)
        {
            Console.WriteLine("Hello!");
        }

    }
}
