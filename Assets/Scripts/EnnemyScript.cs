using UnityEngine;
using System.Collections;

public class EnnemyScript : MonoBehaviour {

	public float speed;
	public int vie;
	public GameObject cible;
	public float callibrageTime;
	public GameObject interfaceShape;
	public bool isNear;
	private Vector3 pos;
	Quaternion targetRotation;

	//

	public GameObject player;
	//this game object's transform  
	private Transform goTransform;  
	//the game object's position on the screen, in pixels  
	private Vector3 goScreenPos;  
	//the game objects position on the screen  
	private Vector3 goViewportPos;  
	
	//the width of the speech bubble  
	public int bubbleWidth = 100;  
	//the height of the speech bubble  
	public int bubbleHeight = 100;  
	
	//an offset, to better position the bubble  
	public float offsetX = 0;  
	public float offsetY = 0;  
	
	//an offset to center the bubble  
	private int centerOffsetX;  
	private int centerOffsetY;  
	
	//a material to render the triangular part of the speech balloon
	public Vector3 truePos;
	public Material mat;  
	//a guiSkin, to render the round part of the speech balloon  
	public GUISkin guiSkin; 
	
	public Texture lockImage;
	public Texture lockImageLaser;

	private float distanceFromPlayer;


	void Awake ()  
	{  
		//get this game object's transform  
		goTransform = this.GetComponent<Transform>();  
	}  

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");
		//Calculate the X and Y offsets to center the speech balloon exactly on the center of the game object  
		centerOffsetX = bubbleWidth/2;  
		centerOffsetY = bubbleHeight/2; 

		speed = speed * Random.Range (0.1f, 4.2f);
		cible = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating("lookAtTarget",0,callibrageTime);
		InvokeRepeating("updateTexture",0,0.1f);
		//InvokeRepeating("setAngle",0,2f);
	}

	void FixedUpdate () {
		rigidbody.AddForce (transform.forward*speed);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
	}

	
	// Update is called once per frame
	void Update () {
		if (distanceFromPlayer < 40)isNear = true;
		else isNear = false;


	}

	void updateTexture(){
		float x = renderer.material.GetTextureOffset("_MainTex").x+Random.Range(1,3);
		float y = renderer.material.GetTextureOffset("_MainTex").y+Random.Range(1,3);
		renderer.material.SetTextureOffset("_MainTex", new Vector2(x,y));
	}

	void lookAtTarget(){

		targetRotation = Quaternion.LookRotation(cible.transform.position - transform.position);
		
		// Smoothly rotate towards the target point.

	}

	public void destroySelf(){
		vie--;

		if(vie <= 0)StartCoroutine ("die");
	}

	void OnBecameInvisible () {

	}

	void LateUpdate()  
	{  
		//find out the position on the screen of this game object  
		goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);   
		
		//Could have used the following line, instead of lines 70 and 71  
		//goViewportPos = Camera.main.WorldToViewportPoint(goTransform.position);  
		goViewportPos.x = goScreenPos.x/(float)Screen.width;  
		goViewportPos.y = goScreenPos.y/(float)Screen.height;  
	}  

	void OnGUI()  
	{  
		truePos = new Vector2(goScreenPos.x, Screen.height - goScreenPos.y);
		if (renderer.isVisible && !GameObject.Find("Ship").GetComponent<RadarModuleScript>().guiIsOff) {
			
			//Begin the GUI group centering the speech bubble at the same position of this game object. After that, apply the offset  
			GUI.BeginGroup (new Rect (goScreenPos.x, Screen.height - goScreenPos.y, bubbleWidth, bubbleHeight));  
			
			//Render the round part of the bubble  
			//GUI.Label(new Rect(0,0,200,100),"",guiSkin.customStyles[0]);  
			
			//Render the text
			distanceFromPlayer = Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").transform.position);

			string pos = ((int)Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").transform.position)).ToString ();
			if(GameObject.Find("Ship").GetComponent<RadarModuleScript>().distanceIsOff){
				
				pos = ((int)Random.Range(100,1000)).ToString();
			}



			GUI.Label (new Rect (0, 0, 190, 50), pos, guiSkin.label);  
			
			GUI.EndGroup ();
			GUI.DrawTexture (new Rect (goScreenPos.x-15f, Screen.height - goScreenPos.y-15f, bubbleWidth, bubbleHeight), lockImage);

			if(isNear)GUI.DrawTexture (new Rect (goScreenPos.x-30f, Screen.height - goScreenPos.y-30f, bubbleWidth*2, bubbleHeight*2), lockImageLaser);


		}
	}  

	IEnumerator die() {
			transform.audio.Play ();
			transform.particleSystem.Play ();
			//transform.renderer.enabled = false;
			yield return new WaitForSeconds(transform.audio.clip.length);
			GameObject.Destroy (gameObject);
		
	}
	
	
}
