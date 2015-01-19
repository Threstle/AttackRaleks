using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public int speed;
	// Use this for initialization
	void Start () {
		speed += (int) GameObject.Find ("Ship").rigidbody.velocity.magnitude;
		InvokeRepeating("autoDestruct",5,1);
	}
	
	// Update is called once per frame
	void Update () {
		speed += 100;
		rigidbody.AddForce (transform.forward * speed);
	}

	void OnTriggerEnter(Collider collision){
		if (collision.tag == "Ennemy") {
			collision.gameObject.GetComponent<EnnemyScript>().destroySelf();
		}

	}

	public void autoDestruct(){
		Destroy(gameObject);
	}

}
