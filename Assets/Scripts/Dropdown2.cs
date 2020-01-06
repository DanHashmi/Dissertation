using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using System.Text;

namespace fishtanksoftware
{

public class Dropdown2 : MonoBehaviour
{
   
    public TextAsset TemperateTankResidents;
    public TextAsset TropicalTankResidents;
    public TextAsset ColdwaterTankResidents;
    public TextAsset CSVText;
    List<TempFish> TempResidents = new List<TempFish>();
    List<TempFish> TempResidents2 = new List<TempFish>();

    public Dropdown dropdown;
    public Dropdown dropdown1;
    private List<Dropdown.OptionData> dropdownOptions;
    public Text SelectedName;
    public InputField EraseField;
    public Text inputFieldText;

    // [SerializeField]
    // private InputField inputField1;

    // [SerializeField]
    // private AutoCompleteComboBox combobox;

    [SerializeField]
    private DropDownListItem combobox;

    private int intVall;
    private int Fpoints;
    private string Fname;
    private int testpoints;
    private string Trafficlights;
    private string addtofile;
    private int[] intArray1;
    private int[] TestArray;
    private int[] sumArray;
    private int[] TrueNumber;
    private string[] csvdata;
    private bool result;
    private int index1;
    private int sum1;
    private int j;
    private int truenumber;
    private string filepath = @"Assets\Scripts\CSVTextFile.txt";

    public void DropDown_IndexChanged(int index)
    {
        SelectedName.text = TempResidents[index].Name;
        if(intArray1[index] == 0)
        {
            SelectedName.color = Color.red;    
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

        UpdateInspector();
        Debug.Log(intArray1[7]);
    }
     
    void PopulateList(string[] data)
    {
        intArray1 = new int[data.Length - 1];

        for(int i = 0; i < intArray1.Length; i++)
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
                // intArray1 = new int[data.Length - 1];
                // for (j = 0; j < intArray1.Length; j += 1)
                // {
                //     intArray1[j] = j; 
                // }
                intArray1[i] = i;
            }
        }
    }
    
    public void AddIt()
    {
        if(dropdown.value != 0)
        {
            if(inputFieldText.text != "")
            {
                addtofile = dropdown.options[dropdown.value].text;
                dropdown1.options.Add(new Dropdown.OptionData(){text = inputFieldText.text + " " + addtofile + ":" + Trafficlights});
                intVall = Convert.ToInt32(inputFieldText.text);
                sum1 = Fpoints * intVall;
                Debug.Log("In total, " + Fname + " is " + sum1 + " points! Traffic Lights Colour = " + Trafficlights);
                EraseField.text = "";
                dropdown1.RefreshShownValue();
                AddCSV(addtofile, sum1, Trafficlights, intVall, filepath);
                UpdateInspector();
                // var lineCount = File.ReadLines(filepath).Count();               
                // Debug.Log(lineCount);
            }
            else
            {
                Debug.Log("Please enter a number");
            }
        }
        else
        {
            Debug.Log("Please select Fish Species");
        }
    }

    public void setIndex(int varIndex)
    {
        this.index1 = varIndex; 
    }

    public void DeleteIt()
    {
        List<Dropdown.OptionData> list = dropdown1.options;
        if(list.Count != 0)
        {
            if(index1 < list.Count)
            {
                string Deleted = dropdown1.options[index1].text;
                dropdown1.options.RemoveAt(index1); 
                Debug.Log("Removed " + Deleted);
                dropdown1.RefreshShownValue();
                DeleteLine(index1);
                UpdateInspector();
            }
            else if(index1 >= list.Count)
            {
                Debug.Log("Please chose which fish to delete"); // have to selcect which one to delete
            }
        }
        else
        {
            Debug.Log("List is Empty");
        } 
    }

    public void CheckIt()
    {
        csvdata = CSVText.text.Split(new char[] {'\n'} );
        PopulateSecond(csvdata);
    }

    void UpdateInspector()
    {
        AssetDatabase.ImportAsset(filepath);
    }

    public void ClearIt()
    {
        dropdown1.ClearOptions();
        System.IO.File.WriteAllText(filepath,string.Empty);
        UpdateInspector();
    }

    void OnApplicationQuit()
    {
        System.IO.File.WriteAllText(filepath,string.Empty);
        UpdateInspector();
    }

    void AddCSV(string Name, int PointsValue, string TrafficLightsColour, int Number, string filepath)
    {
       try
       {
           using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
           {
               file.WriteLine(Name + "," + PointsValue + "," + TrafficLightsColour + "," + Number);
               file.Close(); //not needed
           }
       }
       catch (Exception ex)
       {
           
           throw new ApplicationException("Error :", ex);
       }
    }
    
    void PointsSumCheck(int sum)
    {
        int intAquarium = PlayerPrefs.GetInt("AquariumPoints");

        if(sum <= intAquarium)
        {
            Debug.Log("Aquarium good to go!");
            int checking = 1;
            PlayerPrefs.SetInt("SumCheck", checking);
        }
        else
        {
            int checking = 0;
            PlayerPrefs.SetInt("SumCheck", checking);
            Debug.Log("Aquarium Overcrowded - Please change fish or aquarium selection");
        }
    }
    
    void RetrieveValues()
    {
        int index = dropdown.value;

        for(int i = 1; i < intArray1.Length; i++)
        {
            if(index == intArray1[i])
            {
                Fpoints = TempResidents[i].PointsValue;
                Fname = TempResidents[i].Name;
                Debug.Log(Fname + " is " + Fpoints + " points");
                Trafficlights = TempResidents[i].TrafficLightsColour;
            }
        }     
    }
     void PopulateSecond(string[] data)
    {
        int sum = 0;
        int sum1 = 0;
        for(int i = 0; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] {','});
            
            if (row [0] != "") // Check if row is empty
            {
                TempFish temp = new TempFish();
                temp.Name = row[0];
                int.TryParse(row[1], out temp.PointsValue);
                List<int> sumvalues = new List<int>(){temp.PointsValue};
                temp.TrafficLightsColour = row[2];
                if(temp.TrafficLightsColour[i] == 'A')
                {
                    Debug.Log("Some fish need consideration");
                }
                int.TryParse(row[3], out temp.FishNumber);
                TempResidents2.Add(temp);
                TestArray = new int[data.Length - 1];
                sumArray = new int[data.Length - 1];
                TestArray[i] = temp.PointsValue;
                sum += TestArray[i];
                sumArray[i] = temp.FishNumber;
                sum1 += sumArray[i];
            }
        }
        Debug.Log("Overall fish points total is " + sum);
        PointsSumCheck(sum);
        PlayerPrefs.SetInt("FishNumber", sum1);
    }

    void DeleteLine(int number)
    {
        var file = new List<string>(System.IO.File.ReadAllLines(filepath));
        file.RemoveAt(number);
        File.WriteAllLines(filepath, file.ToArray());
    }
}
}