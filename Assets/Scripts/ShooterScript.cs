using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour {
	public bool canShoot = true;
	public GameObject bullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void shoot(){
		if (canShoot) {
			/*if (!transform.audio.isPlaying) {
				transform.audio.Play ();
			}*/
			Vector3 pos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
			GameObject currentBullet = Instantiate (bullet, pos, transform.rotation) as GameObject;
//			currentBullet.GetComponent<BulletScript> ().speed += transform.parent.GetComponent<ShipScript>().speed;
			currentBullet.transform.parent = transform;
		}
	}
}
