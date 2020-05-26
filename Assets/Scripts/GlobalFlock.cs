using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.IO;

namespace fishtanksoftware
{
public class GlobalFlock : MonoBehaviour 
{
	//public GameObject[] fishPrefabs;
	public static GameObject[] fishPrefabExample;
	private string filepath = @"Assets\Scripts\CSVTextFile.txt";
	private int[] fishArray;
	//public GameObject fishSchool;
	public static int tankSize = 7;
	static int numFish;
	public static GameObject[] allFish;
	public static Vector3 goalPos = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		numFish = PlayerPrefs.GetInt("FishNumber");
		allFish = new GameObject[numFish];
		fishPrefabExample = new GameObject[numFish];
		RenderSettings.fogDensity = 0.03F;
		RenderSettings.fog = true;
		RenderSettings.fogColor = Camera.main.backgroundColor;
		for (int i = 0; i < numFish; i++) 
		{
			Vector3 pos = new Vector3 (
				UnityEngine.Random.Range(-tankSize, tankSize),
				UnityEngine.Random.Range(-tankSize, tankSize),
				UnityEngine.Random.Range(-tankSize, tankSize)
			);
			fishPrefabExample[i] = Resources.Load (PrefabName(i)) as GameObject;
			//fishPrefabs[UnityEngine.Random.Range (0, fishPrefabs.Length)], pos, Quaternion.identity);
		
			GameObject fish = (GameObject)Instantiate (
				fishPrefabExample[i], pos, Quaternion.identity);
			//fish.transform.parent = fishSchool.transform;
            allFish [i] = fish;
		}
		print(numFish);
	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleGoalPos ();
	}

	void HandleGoalPos() 
	{
		if (UnityEngine.Random.Range(1, 10000) < 50) 
		 {
			goalPos = new Vector3 (
				UnityEngine.Random.Range(-tankSize, tankSize),
				UnityEngine.Random.Range(-tankSize, tankSize),
				UnityEngine.Random.Range(-tankSize, tankSize)
			);
		}
	}
		
	string PrefabName(int number)
	{
		// find out how many different fish
		// find out numbers of each type
		String[] lines = File.ReadAllLines(filepath);
		fishArray = new int[lines.Length];
		for(int i = 0; i < lines.Length; i++)
		{
			string toConvert = (lines[i].Split(',')[4]);
			fishArray[i] = Int32.Parse(toConvert);
		}
		if(number < fishArray[0])
		{
			return "Blue_Fish_02";
		}
		else
		{
			if(number > fishArray[1])
			{
				return "Yellow_Fish_09";
			}
			else
			{
				return "Black_White_Fish";
			}
		}
	}
	void OnApplicationQuit()
	{
		System.IO.File.WriteAllText(filepath,string.Empty);
		AssetDatabase.ImportAsset(filepath);
		PlayerPrefs.SetInt("FilterCheck", 0);
	}
}

}