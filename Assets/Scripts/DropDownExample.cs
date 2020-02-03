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
    public Text Error;
    public Text Console;
    private float floatValue;
    private float litres;
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
        }
    }

    void Start()
    {
        PopulateList();
    }
     
    void PopulateList()
    {
        dropdown.AddOptions(names);
    }
    public void GetInput(string capacity)
    {
        if(dropdown.value != 0)
        {
            bool result = Single.TryParse(capacity, out litres);
            if(result)
            {
                floatValue = litres/2;
                PlayerPrefs.SetFloat("AquariumPoints", floatValue); // must be a float
                input.text = ":)";
                Console.text = ("You Entered " + capacity + " litres");
            }
            else
            {
                Error.text = ("Please enter a real number");
                input.text = "";
            }
        }

        else
        {
            Error.text = ("Select Aquarium type first!");
        }
        
    }
}
}
