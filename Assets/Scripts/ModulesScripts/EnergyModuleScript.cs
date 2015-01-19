using UnityEngine;
using System.Collections;

public class EnergyModuleScript : MonoBehaviour {
	
	public bool energy1 = true;
	public bool energy2 = true;
	public bool energy3 = true;
	public bool energy4 = true;
	public bool energy5 = true;
	public bool isDown;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!energy1 && !energy2 && !energy3 && !energy4 && !energy5) {
						isDown = true;
				} else {
						isDown = false;
				}
	}

	public void takeDamage(){
		int rand = (int)Random.Range (1, 7);
		switch (rand) {
		case(1):energy1=false;break;
		case(2):energy2=false;break;
		case(3):energy3=false;break;
		case(4):energy4=false;break;
		case(5):energy5=false;break;
		default:break;

		}
	}

	//Fonction à appeller quand on veux attribuer de l'énérgie à un module

}
