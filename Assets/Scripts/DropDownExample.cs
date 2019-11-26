using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace fishtanksoftware
{

public class DropDownExample : MonoBehaviour
{
    List<string> names = new List<string>() {"Please Select Aquarium","ColdwaterA", "ColdwaterB", "TemperateA", "TemperateB", "TropicalA", "TropicalB"};
    public Dropdown dropdown;
    public Text SelectedName;
    [SerializeField]
    private float intValue;
    public static int test = 10;
    

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
    public void GetInput(string guess)
    {
        Debug.Log("You Entered " + guess);
        //intValue = Convert.ToInt32(guess);
        bool result = Single.TryParse (guess, out intValue);
        if(result)
        {
            PlayerPrefs.SetString("AquariumPoints", guess);
        }
        else
        {
            Debug.Log("Please enter interger");
        }
        input.text = "";
    }
   
}
}
