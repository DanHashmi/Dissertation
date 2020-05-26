using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using System.IO;
public class UnderWaterEffect : MonoBehaviour {

	//This script enables underwater effects. Attach to main camera.

	//Define variable
	public int underwaterLevel = 7;
    private int[] fishArray;
	//The scene's default fog settings
	private bool defaultFog = RenderSettings.fog;
	private Color defaultFogColor = RenderSettings.fogColor;
	private float defaultFogDensity = RenderSettings.fogDensity;
	private Material defaultSkybox = RenderSettings.skybox;
	private Material noSkybox;

	void Start () 
	{
		//Set the background color
		//Camera.main.backgroundColor = new Color(0.22f, 0.64f, 0.77f, 0.6f);
		//Fog();
	}

	void Fog() 
	{
		RenderSettings.fog = true;
		RenderSettings.fogColor = new Color(0.22f, 0.64f, 0.77f, 0.6f);
		RenderSettings.fogDensity = 0.045f;
		RenderSettings.skybox = noSkybox;
	}
	public void testing()
	{
		string filepath = @"Assets\Scripts\CSVTextFile.txt";
		String[] lines = File.ReadAllLines(filepath);
		fishArray = new int[lines.Length];
		for(int i = 0; i < lines.Length; i++)
		{
			string toConvert = (lines[i].Split(',')[4]);
			fishArray[i] = Int32.Parse(toConvert);
			print(fishArray[i]);
			print(lines.Length);
		}
	}
}