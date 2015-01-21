using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

	public ArduinoScript ard;



	private LineRenderer lineRend;
	// Use this for initialization

	//MODULES
	public ReactorModuleScript reactorModule;
	public RadarModuleScript radarModule;
	public ArmesModuleScript armesModule;
	public EnergyModuleScript energyModule;
	//GAMEPLAY VAR
	public string oldKey = "A";
	//ENERGY



	void Start () {
		ard = transform.GetComponent<ArduinoScript> ();

		InvokeRepeating("updateInterface",0,0.001f);

		//ASSIGN MODULES
		reactorModule = transform.gameObject.GetComponent<ReactorModuleScript> ();
		radarModule = transform.gameObject.GetComponent<RadarModuleScript> ();
		armesModule = transform.gameObject.GetComponent<ArmesModuleScript> ();
		energyModule = transform.gameObject.GetComponent<EnergyModuleScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		checkArduino ();
		reactorModule.checkKeys ();


	

	
//		if (Input.GetKeyDown (KeyCode.X)){
//
//			takeDamage();
//		}
	}

	void checkArduino(){
//		if (ard.fuelPower == "1")
//						reactorModule.move ();
//
//

	

		//HULL
			//hullModule.armScreew = ard.armes_screw;

		//Reacteurs
		reactorModule.speed = ard.speed;
		reactorModule.joyX = ard.joyX;
		reactorModule.joyY = ard.joyY;
		reactorModule.isRepearing = ard.reacteur_repair;
		reactorModule.leftIsActive = ard.reacteur_left;
		reactorModule.rightIsActive = ard.reacteur_right;

		ard.reacteur_armor = reactorModule.armorLeft;

		//Armes
		armesModule.intensiteLight = ard.armes_lightCaptor;
		armesModule.isRepearing = ard.armes_repair;
		armesModule.codeBombTechnician = ard.armes_keypad;
		armesModule.isLaser = ard.isLaser;
		armesModule.wantShoot = ard.wantShoot;
		armesModule.wantBomb = ard.wantBomb;
		if (ard.keypad != oldKey) {
						armesModule.addCharToCode (ard.keypad);
						oldKey = ard.keypad;
				}

		//GENERATEUR
		energyModule.isRepearing = ard.generator_repair;
		energyModule.energyArmes = ard.generator_en_armes;
		energyModule.energyRadar = ard.generator_en_radar;
		energyModule.energyReactor = ard.generator_en_reactors;

		//RADAR
		radarModule.isRepearing = ard.radar_repair;
		radarModule.freqChosen = ard.radar_freq;
	}

	public void takeDamage(){
		int rand = (int)Random.Range (1, 4);
		switch (rand) {
		case(1):reactorModule.damageLeft();break;
		case(2):reactorModule.damageRight();break;
		case(3):radarModule.damage();break;
		
		}
	}




	void shootLaser(){

		GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Ennemy");
		foreach (GameObject ennemy in ennemies) {
			if(ennemy.renderer.isVisible){

				if(ennemy.GetComponent<EnnemyScript>().isNear){
					lineRend.renderer.enabled = true;
					lineRend.SetPosition(0,GameObject.Find("CrossHair").transform.position);
					lineRend.SetPosition(1,ennemy.transform.position);
				}
				
			}
		}
	}
		
	void updateInterface (){
		GameObject cross2 =  GameObject.Find ("CrossHair2").gameObject;
		//cross2.transform.localRotation = Quaternion.Euler (0, 0, transform.localRotation.z);
		//cross2.transform.Rotate();
		cross2.transform.localRotation = Quaternion.Euler (0, 0, transform.rotation.z*360);

	}

}

