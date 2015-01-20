﻿using UnityEngine;
using System.Collections;

public class ArmesModuleScript : MonoBehaviour {

	public int armor = 5;
	public int energy;
	public float energyLaser;
	public bool canShoot;
	public bool wantShoot;
	public bool isLaser;
	public bool canShootLaser;
	public float freq;
	public GameObject bullet;
	public string bombCode;
	public string bombCodePilot;
	public bool bombIsReady;
	public bool wantBomb;
	public int sizeCode;

	// Use this for initialization
	void Start () {
		bombCode = "12345";
		StartCoroutine ("reload");
		InvokeRepeating("emptyLaser",0,0.5f);
	}
	
	// Update is called once per frame
	void Update () {

		calculateState ();
//
//		if (Input.GetKey (KeyCode.Space)){
//		    wantShoot = true;
//		}
//		else{
//			wantShoot = false;
//		}
//	
		shootLaser();

	}

	void calculateState(){
		canShoot = true;
		canShootLaser = true;

		if (armor <= 0) {
			canShoot = false;
			canShootLaser = false;
		}

		if (energy <= 0)canShoot = false;
		if (energyLaser <= 0)canShootLaser = false;
		freq = 1 - ((float)energy / 5);
		freq += 1 - ((float)armor / 5);

	}

	public void addCharToCode(string ch){

		if (bombCodePilot.Length-1 > bombCode.Length)bombCodePilot = "";
		bombCodePilot += ch;

		if (bombCodePilot.Equals (bombCode)) {
						bombIsReady = true;
						Debug.Log("BOMB READY TO LAUNCH WAITING FOR TECHNICAL APPROVAL");
				}
	}

	public void launchBomb(){

	}

	public void shoot(){
		if (canShoot && wantShoot) {
			if(isLaser){
				if (canShootLaser) {
					
					GameObject.Find ("Laser").particleSystem.Play();
					GameObject.Find ("Laser").collider.enabled = true;
				}
			}
			else{

			Vector3 pos = new Vector3(transform.localPosition.x+10,transform.localPosition.y,transform.localPosition.z);
			GameObject currentBullet = Instantiate (bullet, pos, transform.rotation) as GameObject;
			//			currentBullet.GetComponent<BulletScript> ().speed += transform.parent.GetComponent<ShipScript>().speed;
			//currentBullet.transform.parent = transform;

			pos = new Vector3(transform.localPosition.x-10,transform.localPosition.y,transform.localPosition.z);
			currentBullet = Instantiate (bullet, pos, transform.rotation) as GameObject;
			//			currentBullet.GetComponent<BulletScript> ().speed += transform.parent.GetComponent<ShipScript>().speed;
			//	currentBullet.transform.parent = transform;
			}
		}
		else {
			GameObject.Find ("Laser").particleSystem.Stop();
			GameObject.Find ("Laser").collider.enabled = false;
			GameObject.Find("Laser").particleSystem.Clear();
		}
	}
	
	IEnumerator desactivateBomb() {
		yield return new WaitForSeconds(5);
		bombIsReady = false;
	}
	
	IEnumerator reload() {
		while (true) {
						shoot();

						yield return new WaitForSeconds(freq);
				}
				
	}

	public void emptyLaser(){
		if (isLaser && canShootLaser && wantShoot) {
			energyLaser -=0.01f;
		}
	}

	public void shootLaser(){



	}

}
