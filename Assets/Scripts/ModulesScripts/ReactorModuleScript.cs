﻿using UnityEngine;
using System.Collections;

public class ReactorModuleScript : MonoBehaviour {

	public bool hasCircuit;

	//MAX
	public int maxArmor = 5;
	public int maxEnergy = 5;

	//CURRENT
	public int armorLeft = 0;
	public int armorRight = 0;
	public int armor = 0;
	public int energy;

	public bool canMove;
	public bool leftOnFlame;
	public bool rightOnFlame;

	public bool leftIsActive;
	public bool rightIsActive;

	public bool isRepearing;

	public float speed = 100;
	public float rotationSpeed;
	private float currentRotationSpeed;
	private float veloc;
	private float rotation;
	public int joyX;
	public int joyY;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("repear", 0, 2);
		currentRotationSpeed = rotationSpeed;
		if (!hasCircuit)
						rotationSpeed = 2;
	}
	
	// Update is called once per frame
	void Update () {

		move ();
		calculateStates ();
		checkKeys ();
		veloc = Mathf.Abs (rigidbody.velocity.x) + Mathf.Abs (rigidbody.velocity.y) + Mathf.Abs (rigidbody.velocity.z);
		
		Camera.main.fieldOfView =   Mathf.Lerp (Camera.main.fieldOfView, 90 + veloc / 2, 0.5f);
		veloc /= 20;

		if (veloc > rotationSpeed * 0.7f) {
			veloc = rotationSpeed * 0.8f;
			
		}
	}

	public void damageLeft(){
		if(armorLeft>0)armorLeft--;
	}

	public void damageRight(){
		if(armorRight>0)armorRight--;
	}

	public void repairLeft(){
		if (!leftOnFlame) {
			if (armorLeft < maxArmor)armorLeft++;
		} else {
			if(Random.Range(0,10)>6 && armorLeft < maxArmor)armorLeft++;
		}
	}
	
	public void repairRight(){
		if (!rightOnFlame) {
			if (armorRight < maxArmor)armorRight++;
		} else {
			if(Random.Range(0,10)>6 && armorRight < maxArmor)armorRight++;
		}
	}



	public void calculateStates(){

		armor = (int)(armorLeft + armorRight) / 2;
		speed = ((float)energy / (float)maxEnergy)*100;
//		Debug.Log ((100 - ((float)armorLeft / (float)maxArmor) * 100) / 2);
		speed -= (100 - ((float)armorLeft / (float)maxArmor)*100)/2;
		speed -= (100 - ((float)armorRight / (float)maxArmor)*100)/2;

		if (armorLeft <= 0)
						leftOnFlame = true;
			else leftOnFlame = false;
		if (armorRight <= 0)
						rightOnFlame = true;
			else rightOnFlame = false;


		if ((!leftOnFlame || !rightOnFlame) && (leftIsActive && rightIsActive))
			canMove = true;
		else
			canMove = false;

		if (leftOnFlame) {
			GameObject.Find ("LeftWing").GetComponent<ParticleSystem>().Play ();

				} else {
			GameObject.Find ("LeftWing").GetComponent<ParticleSystem>().Stop ();
				}
		if (rightOnFlame) {
			GameObject.Find ("RightWing").GetComponent<ParticleSystem>().Play ();
		} else {
			GameObject.Find ("RightWing").GetComponent<ParticleSystem>().Stop ();
		}
	}

	public void move(){

		if(canMove)transform.rigidbody.AddForce (transform.forward * speed*2);
		if (leftOnFlame || !leftIsActive) {
			transform.Rotate (Vector3.right * +(rotationSpeed - (veloc)));
			transform.Rotate (Vector3.forward * +(rotationSpeed - (veloc)));
		}
		if (rightOnFlame || !rightIsActive) {
			transform.Rotate (Vector3.right * -(rotationSpeed - (veloc)));
			transform.Rotate (Vector3.forward * -(rotationSpeed - (veloc)));
		}
		if (hasCircuit) {
			transform.Rotate (Vector3.forward * -(joyX* rotationSpeed - (veloc)));
			transform.Rotate (Vector3.right * -(joyY* rotationSpeed - (veloc)));
		}

	}

	public void repear(){
		if (isRepearing && armorLeft < 5 && armorRight < 5) {
			armorLeft ++;
			armorRight++;
		}
	}

	public void checkKeys (){
		if (Input.GetKey (KeyCode.LeftShift)){
			move();
		}

		if (Input.GetKey (KeyCode.RightArrow)){
			transform.Rotate (Vector3.forward * -((rotationSpeed) - (veloc)));
		}
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.Rotate (Vector3.forward * +((rotationSpeed) - (veloc)));
		if (Input.GetKey (KeyCode.UpArrow))
			transform.Rotate (Vector3.right * +((rotationSpeed) - (veloc)));
		if (Input.GetKey (KeyCode.DownArrow))
			transform.Rotate (Vector3.right * -((rotationSpeed) - (veloc)));
		
		
		//if (Input.GetKey (KeyCode.X))
		//	shootLaser ();
		//else
		//	transform.Find("Laser").renderer.enabled = false;
		
		if (Input.GetKey (KeyCode.Space)) {
			foreach(Transform child in  gameObject.transform){
				if( child.gameObject.name == "Shooter"){
					child.GetComponent<ShooterScript> ().shoot ();
				}
				
			}
		}
		
		
		
		
	}
}
