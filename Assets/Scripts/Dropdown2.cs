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
    public Text console;
    public Text error;
    public Text nameinfo;
    public Text colourinfo;
    [SerializeField]
    private InputField TopInputField;

    private int intVal;
    private int Fpoints;
    private string Fname;
    private string Trafficlights;
    private string AddWithColour;
    private string StartString;
    private string fishselection;
    private int[] intArray1;
    private int[] TestArray;
    private int[] sumArray;
    private string[] ColourArray;
    private string[] csvdata;
    private int index;
    private int index1;
    private int sum1;
    private int filterindex;
    private string filepath = @"Assets\Scripts\CSVTextFile.txt";
    public float maxWidth = 250f;

    public void DropDown_IndexChanged(int varIndex) //no need for varIndex
    {
        this.index = varIndex;
        fishselection = dropdown.options[index].text;
        SelectedName.text = fishselection;

        if(fishselection == "Select Species")
        {
            SelectedName.color = Color.red;
        }
        else 
        {
            SelectedName.color = Color.white;
            RetrieveValues(fishselection); // issue where filter index does not change
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

        ResizeMenus();
        UpdateInspector();
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
                intArray1[i] = i;
            }
        }
    }
    
    public void Test()
    {
        int check = PlayerPrefs.GetInt("FilterCheck");
        if(check == 1)
        {
            Debug.Log("yes-empty");
        }
        else
        {
            Debug.Log("not empty");
        }
    }
    public void AddIt()
    {
        int check = PlayerPrefs.GetInt("FilterCheck");
        if(check == 0)
        {
            Test1();
        }
        else
        {
            error.text = ("Please select Fish Species");
        }
        
    }
    void Test1()
    {
        string addtofile = dropdown.options[dropdown.value].text;
        if(fishselection != "Select Species" && filterindex != 0)
        {
            if(inputFieldText.text != "")
            {
                if(addtofile == fishselection) //issue if autocomplete is nonsense
                {
                    ColourCheck(addtofile);
                    dropdown1.options.Add(new Dropdown.OptionData(){text = inputFieldText.text + " " + AddWithColour});
                    intVal = Convert.ToInt32(inputFieldText.text);
                    sum1 = Fpoints * intVal;
                    console.text = ("In total, " + Fname + " is " + sum1 + " points! Traffic Lights Colour = " + Trafficlights);
                    EraseField.text = "";
                    dropdown1.RefreshShownValue();
                    AddCSV(addtofile, sum1, Trafficlights, intVal, filepath);
                    UpdateInspector();
                }
                else
                {
                    error.text = ("Please select another fish first then come back to your selection"); //temporary solution
                }
            }
            else
            {
                error.text = ("Please enter a number");
            }
        }
        else
        {
            error.text = ("Please select Fish Species");
        }
    }

    void ColourCheck(string fish)
    {
        string lowercase = Trafficlights.ToLower();
        if(lowercase == "red"|| lowercase == "green")
        {
            AddWithColour = "<color=" + lowercase + ">" + fish + "</color>";
        }
        else
        {
            AddWithColour = "<color=orange>" + fish + "</color>";
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
                console.text = ("Removed " + Deleted);
                dropdown1.RefreshShownValue();
                DeleteLine(index1);
                UpdateInspector();
            }
            else if(index1 >= list.Count)
            {
                error.text = ("Please chose which fish to delete"); // have to select which one to delete
            }
        }
        else
        {
            error.text = ("List is Empty");
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
        PlayerPrefs.SetInt("FilterCheck", 0);
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
        float intAquarium = PlayerPrefs.GetFloat("AquariumPoints");

        if(sum <= intAquarium)
        {
            console.text = ("Aquarium good to go!");
            int checking = 1;
            PlayerPrefs.SetInt("SumCheck", checking);
            FishCompatibility(StartString);
        }
        else
        {
            int checking = 0;
            PlayerPrefs.SetInt("SumCheck", checking);
            console.text = ("Aquarium Overcrowded : Points = " + sum + ". Please change fish or aquarium selection");
        }
    }
    
    void RetrieveValues(string text) //string text parameter maybe
    {
        for(int i = 0; i < intArray1.Length; i++)
        {
            if(text == TempResidents[i].Name)
            {
                filterindex = intArray1[i];
                Fpoints = TempResidents[i].PointsValue;
                Fname = TempResidents[i].Name;
                nameinfo.text = (Fname + " is " + Fpoints + " points");
                Trafficlights = TempResidents[i].TrafficLightsColour;
            }
        }    
    }

     void PopulateSecond(string[] data)
    {
        int sum = 0;
        int sum1 = 0;
        StartString = "";

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
                int.TryParse(row[3], out temp.FishNumber);
                TempResidents2.Add(temp);
                TestArray = new int[data.Length - 1];
                sumArray = new int[data.Length - 1];
                ColourArray = new string[data.Length - 1];
                TestArray[i] = temp.PointsValue;
                sum += TestArray[i];
                sumArray[i] = temp.FishNumber;
                sum1 += sumArray[i];
                ColourArray[i] = temp.TrafficLightsColour;
                StartString += ColourArray[i] + " ";
            }
        }
        console.text = ("Overall fish points total is " + sum);
        PointsSumCheck(sum);
        PlayerPrefs.SetInt("FishNumber", sum1);
    }

    void DeleteLine(int number)
    {
        var file = new List<string>(System.IO.File.ReadAllLines(filepath));
        file.RemoveAt(number);
        File.WriteAllLines(filepath, file.ToArray());
    }

    void FishCompatibility(string text) //need rethinking
    {
        bool RedCheck = text.Contains("Red");
        bool AmberCheck = text.Contains("Amber");
        if(RedCheck == false && AmberCheck == false)
        {
            colourinfo.text = ("Fish Compatible!");
            int checking = 1;
            PlayerPrefs.SetInt("CompatCheck", checking);
        }
        else if(RedCheck == true)
        {
            colourinfo.text = ("Some fish need serious consideration");
        }
        else if(AmberCheck == true)
        {
            colourinfo.text = ("Some fish need consideration");
        }
    }
    void ResizeMenus()
    {
        var widest = 0f;
        foreach (var item in dropdown.GetComponentsInChildren<Text>()) 
        {
            widest = Mathf.Max(item.preferredWidth, widest);
        }
        widest = Mathf.Min(maxWidth, widest);
        dropdown.GetComponent<LayoutElement>().preferredWidth = widest + 40;
        TopInputField.GetComponent<LayoutElement>().preferredWidth = widest + 40;
    } 
}
}