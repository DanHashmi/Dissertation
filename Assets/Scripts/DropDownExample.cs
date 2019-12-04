using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace fishtanksoftware
{

public class DropDownExample : MonoBehaviour
{
    List<string> names = new List<string>() {"Please Select Aquarium","Coldwater", "Temperate", "Tropical"};
    public Dropdown dropdown;
    public Text SelectedName;
    private int intValue;
    private int litres;
    // public static int test = 10;
    private string AqType;
    
    

    [SerializeField]
    private InputField input;
    
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
            AqType = dropdown.options[dropdown.value].text;
            PlayerPrefs.SetString("AquariumType", AqType);
           // Debug.Log("this is " + intValue);
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
    public void GetInput(string capacity)
    {
        if(dropdown.value != 0)
        {
            bool result = Int32.TryParse(capacity, out litres);
            if(result)
            {
                intValue = litres/2;
                PlayerPrefs.SetInt("AquariumPoints", intValue); // must be a float
                input.text = ":)";
                Debug.Log("You Entered " + capacity + " litres");
            }
            else
            {
                Debug.Log("Please enter a real number");
                input.text = "";
            }
        }

        else
        {
            Debug.Log("Select Aquarium type first!");
        }
        
    }
   
}
}
