using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour {
	public bool canShoot = true;
	public GameObject bullet;
	public GameObject cible;
	// Use this for initialization
	void Start () {
		cible = GameObject.Find("Ship");
		InvokeRepeating ("shoot", 1, 0.2f*Random.Range(0.1f,30));
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (1, 200) < 2) {
			shootRandom();
				}
	}

	public void shootRandom(){
		GameObject currentBullet = Instantiate (bullet, transform.position, transform.parent.transform.rotation) as GameObject;
	}

	public void shoot(){
		if (cibleIsNear()) {
			/*if (!transform.audio.isPlaying) {
				transform.audio.Play ();
			}*/

			transform.LookAt(new Vector3(Random.Range(-10,10)+cible.transform.position.x,
			                             Random.Range(-10,10)+cible.transform.position.y,
			                             Random.Range(-10,10)+cible.transform.position.z));
			Vector3 pos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
			GameObject currentBullet = Instantiate (bullet, pos, transform.rotation) as GameObject;
//			currentBullet.GetComponent<BulletScript> ().speed += transform.parent.GetComponent<ShipScript>().speed;
		//	currentBullet.transform.parent = transform;
		}
	}

	public bool cibleIsNear(){

		if ((int)Vector3.Distance (transform.position, GameObject.FindGameObjectWithTag ("Player").transform.position) < 100) {
						return true;
				} else {
						return false;
				}
	}
}
