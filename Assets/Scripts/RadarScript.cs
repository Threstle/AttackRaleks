using UnityEngine;
using System.Collections;

public class RadarScript : MonoBehaviour {

	public GameObject target;
	public Texture aim;
	public Material mat;
	private Vector3 startVertex;
	private Vector3 mousePos;
	// Use this for initialization
	void Start () {
		startVertex = new Vector3(0.5f, 0.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnGUI() {

	
	}

	void OnPostRender() {
		if (!mat) {
			Debug.LogError("Please Assign a material on the inspector");
			return;
		}

		GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Ennemy");

		GL.PushMatrix();
		mat.SetPass(0);
		GL.LoadOrtho();
		GL.Begin(GL.LINES);
		GL.Color(Color.red);

		foreach (GameObject ennemy in ennemies) {
			if(ennemy.renderer.isVisible){

				Vector3 posEnnemy = ennemy.transform.position;
				Vector2 posEnnemy2;
				posEnnemy = Camera.main.WorldToScreenPoint(posEnnemy);
				posEnnemy2 = Camera.main.ScreenToViewportPoint(posEnnemy);
				//posEnnemy.x = posEnnemy.x/(float)Screen.width;  
				//posEnnemy.y = posEnnemy.y/(float)Screen.height; 

				GL.Vertex(posEnnemy2);
				GL.Vertex(startVertex);

			}
		}
		GL.End();
		GL.PopMatrix();






	}

}
