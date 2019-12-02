using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace fishtanksoftware
{

public class Dropdown2 : MonoBehaviour
{
    //LoadData loaddata= new LoadData();
    public TextAsset TemperateTankResidents;
    public TextAsset TropicalTankResidents;
    public TextAsset ColdwaterTankResidents;
    List<TempFish> TempResidents = new List<TempFish>();
    //DropDownExample drop = new DropDownExample();
    //List<string> names = new List<string>() {"Please Select Fish","Neon Tetra", "Guppy", "Goldfish", "Red Snail", "Snail 2", "Shrimp", "Goldfish 1", "Goldfish 2", "Goldfish 3"};
    public Dropdown dropdown;
    public Text SelectedName;
    
    [SerializeField]
    private InputField inputField1;

    private int intVal;
    private int Fpoints;
    
    private bool result;
    //public string guess;
    //public float Points;

    public void DropDown_IndexChanged(int index)
    {
        SelectedName.text = TempResidents[index].Name;

        if(index == 0)
        {
            SelectedName.color = Color.red;
            RetrieveValues();
        }

        else 
        {
            SelectedName.color = Color.white;
            RetrieveValues();
            
        }
    }

    
    void Start()
    {
        string aq = PlayerPrefs.GetString("AquariumType");
        string[] tempdata = TemperateTankResidents.text.Split(new char[] {'\n'} );
        string[] tropdata = TropicalTankResidents.text.Split(new char[] {'\n'} );
        string[] colddata = ColdwaterTankResidents.text.Split(new char[] {'\n'} );

        if(aq == "Temperate")
        {
            PopulateList(tempdata); 
        }
        
        else if(aq == "Tropical")
        {
            PopulateList(tropdata);
        }

        else if(aq == "Coldwater")
        {
            PopulateList(colddata);
        }

        else
        {
            Debug.Log("Please select Aquarium!"); // need to work around go button
        }
    }
     
    void PopulateList(string[] data)
    {
        for(int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] {','});
            
            if (row [1] != "") // Check if row is empty
            {
                TempFish temp = new TempFish();
                int.TryParse(row[0], out temp.ID);
                temp.Name = row[1];
                List<string> names = new List<string>(){temp.Name};
                int.TryParse(row[2], out temp.PointsValue);
                temp.TrafficLightsColour = row[3];
                temp.Skin = row[4];
                temp.Cost = row[5];
                dropdown.AddOptions(names);
                TempResidents.Add(temp);
            }
            
        }
        
    }

    public void GetInput(string guess)
    {
        //Debug.Log("You Entered " + guess);
        //intVal = Convert.ToInt32(guess);
        bool result = Int32.TryParse (guess, out intVal);
        inputField1.text = "";
        if(result)
            {
                 intVal = Convert.ToInt32(guess);
                 PlayerPrefs.SetInt("FishNumber", intVal);  
                 int sum = Fpoints * intVal;
                 Debug.Log("You have " + sum + " Points!");
                 PointsSumCheck(sum);
            }
            else
            {
                Debug.Log("Please enter interger ;(");    
            }

    }
    
    void PointsSumCheck(int sum)
    {
        int intAquarium = PlayerPrefs.GetInt("AquariumPoints");

        if(sum <= intAquarium)
        {
            Debug.Log("Aquarium good to go!");
        }
        else
        {
            Debug.Log("Aquarium Overcrowded");
        }
    }
    
    void RetrieveValues()
    {
        int index = dropdown.value;
        int[] intArray = new int[14]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13}; // must change in future
      
        for(int i = 0; i < 14; i++)
        {
            if(index == intArray[i])
            {
                Debug.Log(TempResidents[i].Name + " is " + TempResidents[i].PointsValue + " points");
                Fpoints = TempResidents[i].PointsValue;
                // dropdown.value = dropdown.value + 1;
                //Debug.Log(intArray[0]);
                //Debug.Log(dropdown.options[dropdown.value].text);
            }
        }     
    }

}

 public static class MultiProfile 
 {
     private static string currentProfile = "default";
     public static int GetInt(string key, int defaultValue) 
     {
         return PlayerPrefs.GetInt(currentProfile + key, defaultValue);
     }
 }
}