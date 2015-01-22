using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour {
	public GameObject monstre;
	public float invokeRate;
	public float max;
	public GameObject player;
	void Start () {
		InvokeRepeating("invokeMonster",15,invokeRate);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void invokeMonster(){
		if (GameObject.FindGameObjectsWithTag("Ennemy").Length < max) {
			Vector3 spawnPos = new Vector3 (
				Random.Range (player.transform.position.x - 50, player.transform.position.x + 50),
				Random.Range (player.transform.position.y - 50, player.transform.position.y + 50),
				Random.Range (player.transform.position.y - 50, player.transform.position.z + 50)
				);
			GameObject monstreInstance = Instantiate (monstre, spawnPos, Quaternion.identity) as GameObject;
			//monstreInstance.transform.parent = transform;
		}
	}
}
