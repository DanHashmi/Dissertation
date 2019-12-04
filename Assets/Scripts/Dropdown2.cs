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
    private string Trafficlights;
    private int NumRows;
    private int[] intArray1;
    private bool result;
    //public string numberOfFish;
    //public float Points;

    public void DropDown_IndexChanged(int index)
    {
        SelectedName.text = TempResidents[index].Name;

        if(index == 0)
        {
            SelectedName.color = Color.red;    
            //RetrieveValues();
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
        for(int i = 0; i < data.Length - 1; i++)
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
                int.TryParse(row[5], out temp.Cost);
                dropdown.AddOptions(names);
                TempResidents.Add(temp);
                intArray1 = new int[data.Length - 1];
                for (var j = 0; j < intArray1.Length; j += 1)
                {
                    intArray1[j] = j; // must change in future
                }
                //NumRows = lines.Length;
                //intArray = 
            }
            
        }
        
    }

    public void GetInput(string numberOfFish)
    {
        if(dropdown.value != 0)
        {
            bool result = Int32.TryParse (numberOfFish, out intVal);
            inputField1.text = "";
            if(result)
            {
                intVal = Convert.ToInt32(numberOfFish);
                PlayerPrefs.SetInt("FishNumber", intVal);   // create array of prefs?
                int sum = Fpoints * intVal;
                Debug.Log("You have " + sum + " Points! Traffic Lights Colour = " + Trafficlights);
                PointsSumCheck(sum);
            }
            else
            {
                Debug.Log("Please enter interger ;(");    
            }
        }
        
        else
        {
            Debug.Log("Please Select a fish species!");
        }
        
    }
    
    void PointsSumCheck(int sum)
    {
        int intAquarium = PlayerPrefs.GetInt("AquariumPoints");

        if(sum <= intAquarium)
        {
            Debug.Log("Aquarium good to go!");
            var checking = true;
            PlayerPrefs.SetInt("SumCheck", checking?1:0);
        }
        else
        {
            Debug.Log("Aquarium Overcrowded");
        }
    }
    
    void RetrieveValues()
    {
        int index = dropdown.value;

        for(int i = 1; i < intArray1.Length; i++)
        {
            if(index == intArray1[i])
            {
                Debug.Log(TempResidents[i].Name + " is " + TempResidents[i].PointsValue + " points");
                Fpoints = TempResidents[i].PointsValue;
                Trafficlights = TempResidents[i].TrafficLightsColour;
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