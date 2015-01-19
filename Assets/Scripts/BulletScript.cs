using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public int speed;
	public int lifeTime;
	public bool isEnnemy;

	// Use this for initialization
	void Start () {

		if(!isEnnemy)speed += (int) GameObject.Find ("Ship").rigidbody.velocity.magnitude;
		InvokeRepeating("autoDestruct",lifeTime,1);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isEnnemy)speed += 100;
		rigidbody.AddForce (transform.forward * speed);
	}

	void OnTriggerEnter(Collider collision){

		if (collision.tag == "Ennemy"  && !isEnnemy) {

			collision.gameObject.GetComponent<EnnemyScript>().destroySelf();
		}
		if (collision.tag == "Player"  && isEnnemy) {
		
			Debug.Log("TOUCHE");
		}

	}

	public void autoDestruct(){
		Destroy(gameObject);
	}

}
