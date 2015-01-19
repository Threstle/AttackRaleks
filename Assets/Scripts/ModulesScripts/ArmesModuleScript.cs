using UnityEngine;
using System.Collections;

public class ArmesModuleScript : MonoBehaviour {

	public int armor = 5;
	public int energy;
	public float energyLaser;
	public bool canShoot;
	public bool wantShoot;
	public bool wantShootLaser;
	public bool canShootLaser;
	public float freq;
	public GameObject bullet;

	public string bombCode = "5876146";

	// Use this for initialization
	void Start () {
		StartCoroutine ("reload");
		InvokeRepeating("emptyLaser",0,0.5f);
	}
	
	// Update is called once per frame
	void Update () {

		calculateState ();

		if (Input.GetKey (KeyCode.Space)){
		    wantShoot = true;
		}
		else{
			wantShoot = false;
		}
	
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

	public void shoot(){
		if (canShoot && wantShoot) {
			/*if (!transform.audio.isPlaying) {
				transform.audio.Play ();
			}*/

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



	IEnumerator reload() {
		while (true) {
						shoot();

						yield return new WaitForSeconds(freq);
				}
				
	}

	public void emptyLaser(){
		if (wantShootLaser && canShootLaser) {
			energyLaser -=0.01f;
		}
	}

	public void shootLaser(){

		if (wantShootLaser && canShootLaser) {
					
						GameObject.Find ("Laser").particleSystem.Play();
						GameObject.Find ("Laser").collider.enabled = true;
				} else {
						GameObject.Find ("Laser").particleSystem.Stop();
						GameObject.Find ("Laser").collider.enabled = false;
			GameObject.Find("Laser").particleSystem.Clear();
				}

	}

}
