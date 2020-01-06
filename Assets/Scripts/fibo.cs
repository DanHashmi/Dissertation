using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using System.Text;
using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

public class fibo : MonoBehaviour
{
     int truenumber;
    // Start is called before the first frame update
    public void Starting()
    {
      AddCSV("Dan", 9 , "Green", @"Assets\Scripts\test.txt");
      AddCSV("Dani", 9 , "Green", @"Assets\Scripts\test.txt");
      

       AssetDatabase.ImportAsset(@"Assets\Scripts\test.txt"); 
    }

    public void Delete()
    {
    string line = null;
    int line_number = 0;
    int line_to_delete = 4;

    using (StreamReader reader = new StreamReader(@"Assets\Scripts\test.txt")) 
    {
    using (StreamWriter writer = new StreamWriter(@"Assets\Scripts\t.txt")) 
    {
        while ((line = reader.ReadLine()) != null) 
        {
            line_number++;

            if (line_number == line_to_delete)
                continue;

            writer.WriteLine(line);
        }
    }
    }
    }

    public void AddCSV(string Name, int PointsValue, string TrafficLightsColour, string filepath)
    {
       string[] lines = System.IO.File.ReadAllLines(@"Assets\Scripts\test.txt");
        if(lines.Length != 0)
        {
        for(int i=0; i<lines.Length; i++) 
        {
         if(lines[i] == string.Empty)
         {
             truenumber = i;
             break;
         }  
         }
         Debug.Log(truenumber);
         Debug.Log(lines.Length);
        }
        else
        {
            print("fucked it");
        }
    if(lines[truenumber] == string.Empty)
    {
       try
       {
           using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
           {
               file.WriteLine(Name + "," + PointsValue + "," + TrafficLightsColour);
           }
       }
       catch (Exception ex)
       {
           
           throw new ApplicationException("Error :", ex);
       }
    }
    else{
        Debug.Log("we tried");
    }
    }
    
    // void Pascal(int Rows)
    // {
    //     int[][] jagged_arr = new int[Rows][];
    //     jagged_arr[0] = new int[] {1};
    //     jagged_arr[1] = new int[] {1, 1};

    //     for(int i = 2; i < Rows; i++)
    //     {
    //         int a = Convert.ToInt32(jagged_arr[i-1]);
    //         int b = Convert.ToInt32(jagged_arr[i-2]);
    //         int c = a + b;
    //         jagged_arr[i] = new int[] {c};
    //         Debug.Log(jagged_arr[i-1]);
    //     }
    //     Debug.Log(jagged_arr);
    // }



//    private string getPath(){
//         #if UNITY_EDITOR
//         return Application.dataPath +"/Assets/"+"/Scripts/"+"CSVTextFile.txt;
//         #elif UNITY_ANDROID
//         return Application.persistentDataPath+"Saved_data.csv";
//         #elif UNITY_IPHONE
//         return Application.persistentDataPath+"/"+"Saved_data.csv";
//         #else
//         return Application.dataPath +"/"+"Saved_data.csv";
//         #endif
//     }
}



