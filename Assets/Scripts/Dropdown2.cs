using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace fishtanksoftware
{

public class Dropdown2 : MonoBehaviour
{
    //trying trying1 = new trying();
    //DropDownExample drop = new DropDownExample();
    List<string> names = new List<string>() {"Please Select Fish","Neon Tetra", "Guppy", "Goldfish", "Red Snail", "Snail 2", "Shrimp", "Goldfish 1", "Goldfish 2", "Goldfish 3"};
    public Dropdown dropdown;
    public Text SelectedName;
    public int intAquarium;
    
    [SerializeField]
    private InputField inputField1;

    public float intVal;
    
    public bool result;
    //public string guess;
    public float Points;

    public void DropDown_IndexChanged(int index)
    {
        SelectedName.text = names[index];

        if(index == 0)
        {
            SelectedName.color = Color.red;
        }

        // else if(index == 1)
        // {
        //     Debug.Log("please");  
        // }
        
        else 
        {
            SelectedName.color = Color.white;
            string madness = PlayerPrefs.GetString("AquariumPoints");
            intAquarium = Convert.ToInt32(madness);
            //Debug.Log($"Please work {intAquarium}");
            
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
        //Debug.Log("You Entered " + guess);
        //intVal = Convert.ToInt32(guess);
        bool result = Single.TryParse (guess, out intVal);
        inputField1.text = "";
        if(result)
            {
                 intVal = Convert.ToInt32(guess);
                 PlayerPrefs.SetFloat("FishNumber", intVal);
                 if(dropdown.value == 1)
                 {
                 int NeonTetra = 2;
                 Points = intVal * NeonTetra;
                 Debug.Log($"You have {Points} Points");
                 PointsSumCheck(); 
                 }
                 else
                 {
                     Debug.Log("Please select Neon Tetra");
                 }
            }
            else
            {
                Debug.Log("Please enter interger ;(");    
            }

    }
    
    void PointsSumCheck()
    {
        if(Points <= intAquarium)
        {
            Debug.Log("Aquarium good to go!");
        }
        else
        {
            Debug.Log("Aquarium Overcrowded");
        }
    }
    

public class trying : DropDownExample
    {
         public void method1()
         {
             //DropDownExample DDE = new DropDownExample();
             Debug.Log("This is ");
             
         }
    }
}

}