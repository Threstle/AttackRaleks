using UnityEngine;
using System.Collections;

public class EnergyModuleScript : MonoBehaviour {
	
	public int energyReactor;
	public int energyArmes;
	public int energyRadar;
	public int armor;
	public bool isRepearing;
	public bool isDown;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("repear", 0, 2);
	}
	
	// Update is called once per frame
	void Update () {
		checkState ();

	}

	public void checkState(){

		energyReactor = (int)(energyReactor * (armor/5.0));
		energyArmes = (int)(energyArmes * (armor/5.0));
		energyRadar = (int)(energyRadar * (armor/5.0));
	}

	public void takeDamage(){
		armor --;
	}

	public void repear(){
		if (isRepearing && armor < 5) {
			armor ++;
		}
	}


	//Fonction à appeller quand on veux attribuer de l'énérgie à un module

}
