using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Dropdown2 : MonoBehaviour
{
    List<string> names = new List<string>() {"Please Select Fish","Neon Tetra", "Guppy", "Goldfish", "Red Snail", "Snail 2", "Shrimp", "Goldfish 1", "Goldfish 2", "Goldfish 3"};
    public Dropdown dropdown;
    public Text SelectedName;
    // public InputField inputField;

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
