﻿using UnityEngine;
using System.Collections;

public class RadarModuleScript : MonoBehaviour {

	public int armor = 5;
	public int energy=1;
	public bool isBroken;
	public bool isOff;
	public bool distanceIsOff;
	public bool guiIsOff;
	public bool isRepearing;
	public int freqChosen;
	public int freqRadar;
	// Use this for initialization
	void Start () {
		freqRadar = Random.Range (1, 5);
		InvokeRepeating("looseConnection",1,5);
		InvokeRepeating ("repear", 0, 2);
	}
	
	// Update is called once per frame
	void Update () {
		calculateStates ();

		if (!isOff) {
				Camera.main.rect = new Rect(0,0,1,1);
						if (isBroken) {
								if (Random.Range (0, 10) > 1) {
										Camera.main.clearFlags = CameraClearFlags.Nothing;
								} else {
										if (Random.Range (0, 5) > 2)
												Camera.main.clearFlags = CameraClearFlags.Color;
										else
												Camera.main.clearFlags = CameraClearFlags.Skybox;
								}


						} else {
								Camera.main.clearFlags = CameraClearFlags.Skybox;
						}

				} else {
					Camera.main.rect = new Rect(0,0,0,0);
				}

	}

	public void damage(){
		armor--;
	}

	public void repear(){
		if (isRepearing && armor < 5) {
			armor ++;
		}
	}

	public void calculateStates(){
		int oldFreq = freqChosen;
			if (freqRadar != freqChosen) {
						isBroken = true;
				} else {
						isBroken = false;
				}
	

		if (energy <= 0) {
						isOff = true;
				} else {
						isOff = false;
					
				}
		switch (armor) {
			case(2):distanceIsOff = true;break;
			case(1):guiIsOff = true;break;
			case(0):isBroken = true;break;
		}


		if (freqRadar != oldFreq)
						GameObject.Find ("Radar").audio.Play ();
	}
	
	void looseConnection(){
				if (Random.Range (0, 50) < 2) {
						freqRadar = Random.Range (1, 5);
				}
		}

}
