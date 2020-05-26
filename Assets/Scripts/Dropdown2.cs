using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;

namespace fishtanksoftware
{
public class Dropdown2 : MonoBehaviour
{
    public TextAsset TemperateTankResidents;
    public TextAsset TropicalTankResidents;
    public TextAsset ColdwaterTankResident;
    public TextAsset CSVText;
    List<TempFish> TempResidents = new List<TempFish>();
    List<TempFish> TempResidents2 = new List<TempFish>();
    public Dropdown dropdown;
    public Dropdown dropdown1;
    public Text SelectedName;
    public InputField EraseField;
    public Text inputFieldText;
    public Text console;
    public TMP_Text UserInterface;
    public Text error;
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
    private int[] testsum;
    private string[] ColourArray;
    private string[] csvdata;
    private string fishinfo;
    private int index1;
    private int sum1;
    private int filterindex;
    public static bool AquariumCheck = false;
    private int newsum;
    private string filepath = @"Assets\Scripts\CSVTextFile.txt";
    public float maxWidth = 250f;

    public void DropDown_IndexChanged(int varIndex) 
    {
        fishselection = dropdown.options[varIndex].text;
        if(fishselection == "Select Species")
        {
            SelectedName.color = Color.red;
            SelectedName.text = fishselection;
        }
        else 
        {
            SelectedName.color = Color.white;
            string fishinformation = RetrieveValues(fishselection);
            SelectedName.text = fishinformation; 
        }
    }

    void Start()
    {
        string aq = PlayerPrefs.GetString("AquariumType");
        string[] tempdata = TemperateTankResidents.text.Split(new char[] {'\n'} );
        string[] tropdata = TropicalTankResidents.text.Split(new char[] {'\n'} );
        string[] colddata = ColdwaterTankResident.text.Split(new char[] {'\n'} );

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
    
    public void Increment() // method to increment selected option 
    {
        bool check = true;
        ChangeValue(check);
    }

    public void Decrement() // method to decrement selected option
    {
        bool check = false;
        ChangeValue(check);
    }
    void ChangeValue(bool check) // change dropdown option which has been incremented/decremented
    {
        if(dropdown1.options.Count != 0) // check if dropdown is empty
        {
            if (index1 < dropdown1.options.Count) // check index not out of range
            {
                string selectedOption = dropdown1.options[index1].text;
                string NumberString = Regex.Match(selectedOption, @"\d+").Value;
                string NoNumberString = Regex.Replace(selectedOption, @"[\d-]", string.Empty);
                int resultNo = Int32.Parse(NumberString);
                if(check == true)
                {
                    int incrementedNo = resultNo + 1;
                    AddNewItem(incrementedNo, NoNumberString);
                    ChangeBackend(NumberString, incrementedNo, check);
                }
                else
                {
                    int decrementedNo = resultNo - 1;
                    AddNewItem(decrementedNo, NoNumberString);
                    ChangeBackend(NumberString, decrementedNo, check);
                }
            }
            else
            {
                console.text = ("Please select fish from dropdown menu");
            }
        }
        else
        {
           console.text = ("List is Empty");
        }
    }
    void AddNewItem(int change, string OnlyCharacters) //adds new item to menu
    {
        Dropdown.OptionData newItem = new Dropdown.OptionData(change + OnlyCharacters);
        dropdown1.options.RemoveAt(index1);
        dropdown1.options.Insert(index1, newItem);
        dropdown1.RefreshShownValue();
    }
    public void AddIt() // add new fish species
    {
        int check = PlayerPrefs.GetInt("FilterCheck");
        if(check == 0)
        {
            AddNewSpecies();
        }
        else
        {
            console.text = ("Please select Fish Species");
        }
        
    }
    void ChangeBackend(string number, int increment, bool check) // changes csv file
    {
        //Read all lines from text file
        String[] lines = File.ReadAllLines(filepath);
        if(increment > 0)
        {
            for(int i = 0; i <= lines.Length; i++) 
            {  
                if(i == index1)
                {
                    string sum = (lines[i].Split(',')[3]);
                    string PointsNo = (lines[i].Split(',')[1]);
                    int strlength = number.Length + sum.Length + 1;
                    int intsum = Int32.Parse(sum);
                    int intPoints = Int32.Parse(PointsNo);
                    if(check == true)
                    {
                        newsum = (intsum + intPoints);
                    }
                    else
                    {
                        newsum = (intsum - intPoints);
                    }
                    lines[i] = lines[i].Substring(0, lines[i].Length-strlength) + newsum + "," + increment;
                }
            }
        File.WriteAllLines(filepath, lines); // re-write it to file
        }
        else // delete once number of fish hits zero
        {
            DeleteLine(index1);
            dropdown1.options.RemoveAt(index1); 
        }
        UpdateInspector();
    }
    void AddNewSpecies()
    {
        string addtofile = dropdown.options[dropdown.value].text; 
        if(fishselection != "Select Species" && filterindex != 0)
        {
            if(inputFieldText.text != "")
            {
                if(addtofile == fishselection) //issue if autocomplete is nonsense
                {
                    ColourCheck(addtofile);
                    dropdown1.options.Add(new Dropdown.OptionData(){text = inputFieldText.text + " " +AddWithColour});
                    intVal = Convert.ToInt32(inputFieldText.text);
                    sum1 = Fpoints * intVal;                
                    UserInterface.text = ("In total, " + Fname + " is " + sum1 + " points! Traffic Lights Colour = " + Trafficlights);
                    EraseField.text = "";
                    dropdown1.RefreshShownValue();
                    AddCSV(addtofile, Fpoints, Trafficlights, sum1, intVal, filepath);
                    UpdateInspector();
                }
                else
                {
                    error.text = ("Please select another fish first then come back to your selection"); //temporary solution
                }
            }
            else
            {
                console.text = ("Please enter a number");
            }
        }
        else
        {
            console.text = ("Please select Fish Species");
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
        print(index1);
    }

    public void DeleteIt()
    {
        if(dropdown1.options.Count != 0)
        {
            if(index1 < dropdown1.options.Count)
            {
                string Deleted = dropdown1.options[index1].text;
                dropdown1.options.RemoveAt(index1); 
                console.text = ("Removed " + Deleted);
                dropdown1.RefreshShownValue();
                DeleteLine(index1);
                UpdateInspector();
            }
            else if(index1 >= dropdown1.options.Count)
            {
                console.text = ("Please chose which fish to delete"); // have to select which one to delete
            }
        }
        else
        {
            console.text = ("List is Empty");
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
        index1 = 0;
        UpdateInspector();
    }

    void OnApplicationQuit()
    {
        System.IO.File.WriteAllText(filepath,string.Empty);
        UpdateInspector();
        PlayerPrefs.SetInt("FilterCheck", 0);
    }

    void AddCSV(string Name, int IndividualValue, string TrafficLightsColour, int PointsValue, int Number, string filepath)
    {
       try
       {
           using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
           {
               file.WriteLine(Name + "," + IndividualValue + "," + TrafficLightsColour + "," + PointsValue + "," + Number);
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
            UserInterface.text = ("Aquarium good to go! Points = " + sum);
            AquariumCheck = true;
            //int checking = 1;
            //PlayerPrefs.SetInt("SumCheck", checking);
            FishCompatibility(StartString);
        }
        else
        {
            //int checking = 0;
            //PlayerPrefs.SetInt("SumCheck", checking);
            AquariumCheck = false;
            UserInterface.text = ("Aquarium Overcrowded : Points = " + sum + ". Please change fish or aquarium selection");
        }
    }
    
    string RetrieveValues(string text) //could have it for 2nd dropdown too
    {
        for(int i = 0; i < intArray1.Length; i++)
        {
            if(text == TempResidents[i].Name)
            {
                filterindex = intArray1[i];
                Fpoints = TempResidents[i].PointsValue;
                Fname = TempResidents[i].Name;
                Trafficlights = TempResidents[i].TrafficLightsColour;
                fishinfo = (Fname + " is " + Fpoints + " points");
            }
        } 
        return fishinfo;   
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
                int.TryParse(row[1], out temp.IndPointsValue);
                temp.TrafficLightsColour = row[2];
                int.TryParse(row[3], out temp.PointsValue);
                int.TryParse(row[4], out temp.FishNumber);
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
        UserInterface.text = ("Overall fish points total is " + sum);
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