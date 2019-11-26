using UnityEngine;
using System.Collections;
using System;

namespace fishtanksoftware
{
	
public class GlobalFlock : MonoBehaviour 
{
    //Please please = new Please();
	public GameObject defaultFish;
	public GameObject[] fishPrefabs;
	public GameObject fishSchool;
	public static int tankSize = 7;

	static int numFish = 20;
	//static int numberFish = Convert.ToInt32(numFish);
	public static GameObject[] allFish = new GameObject[numFish];
	public static Vector3 goalPos = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		// RenderSettings.fogDensity = 0.03F;
		// RenderSettings.fog = true;
		// RenderSettings.fogColor = Camera.main.backgroundColour;
		// int nombre = please.Nfish();
		// AllFish = please.mad();
		for (int i = 0; i < numFish; i++) 
		{
			Vector3 pos = new Vector3 (
				UnityEngine.Random.Range(-tankSize, tankSize),
				UnityEngine.Random.Range(-tankSize, tankSize),
				UnityEngine.Random.Range(-tankSize, tankSize)
			);
			GameObject fish = (GameObject)Instantiate (
				fishPrefabs[UnityEngine.Random.Range (0, fishPrefabs.Length)], pos, Quaternion.identity);
			fish.transform.parent = fishSchool.transform;
            allFish [i] = fish;
		}
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
	
	
}
// public class Please
// {
// 	public static float numFish = PlayerPrefs.GetFloat("FishNumber");
// 	public static int numberFish = Convert.ToInt32(numFish);
// 	public int Nfish()
// 	{
// 		return numberFish;
// 	}
	
// 	public static GameObject[] allFish = new GameObject[numberFish];
// 	public GameObject mad()
// 	{
//         return allFish;
// 	}

// }
}