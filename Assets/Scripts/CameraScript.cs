using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Vector2 shakeRate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x+shakeRate.x,transform.position.y+shakeRate.y,transform.position.z);
	}

	public void shake(float s){
	//	StartCoroutine (shakeCamera (s));
	}
	
	IEnumerator shakeCamera(float s){
		for (int i = 0; i < 50; i++) {
			Vector3 posCam = transform.position;
			shakeRate.x = Random.Range (-s, s);
			shakeRate.y = Random.Range (-s, s);
			yield return new WaitForSeconds (0.02f);
			shakeRate.x = 0;
			shakeRate.y = 0;
		}
	}
}
