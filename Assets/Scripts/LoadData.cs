using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace fishtanksoftware
{
public class LoadData : MonoBehaviour
{
    public TextAsset TemperateTankResidents;
    public List<TempFish> TempResidents = new List<TempFish>();
    public Dropdown dropdown;
 

    // Start is called before the first frame update
    public void Start()
    {
        if(PlayerPrefs.GetString("AquariumType") == "Coldwater")
        {
            Populating(); 
        }
        //Populating();
        else 
        {
            Debug.Log("Go back!");
        }
    }

    public void csv()
    {
        int index = dropdown.value;
        int[] intArray = new int[14]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13}; // must change in future
      
        for(int i = 0; i < 14; i++)
        {
        if(index == intArray[i])
        {
            Debug.Log(TempResidents[i].Name + " is " + TempResidents[i].PointsValue + " points");
            // dropdown.value = dropdown.value + 1;
            //Debug.Log(intArray[0]);
            //Debug.Log(dropdown.options[dropdown.value].text);
            
        }
        }
    }

    public void Populating()
    {
        string[] data = TemperateTankResidents.text.Split(new char[] {'\n'} );
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
                dropdown.AddOptions(names);
                TempResidents.Add(temp);
            }
            
        }
    }
}
}
