using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collision){
		if (collision.tag == "Ennemy") {
			collision.gameObject.GetComponent<EnnemyScript>().destroySelf();
		}
		
	}
}
