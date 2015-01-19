using UnityEngine;  
using System.Collections;  

[ExecuteInEditMode]  
public class GUIScript : MonoBehaviour  
{  
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

	private RadarModuleScript radar;



	//RADAR MODULE VAR
	public bool isOff;
	public bool distanceIsOff;



	//use this for early initialization  
	void Awake ()  
	{  
		//get this game object's transform  
		goTransform = this.GetComponent<Transform>();  
	}  
	
	//use this for initialization  
	void Start()  
	{  
		radar = GameObject.Find ("Ship").GetComponent<RadarModuleScript> ();
		player = GameObject.FindGameObjectWithTag("Player");
		//Calculate the X and Y offsets to center the speech balloon exactly on the center of the game object  
		centerOffsetX = bubbleWidth/2;  
		centerOffsetY = bubbleHeight/2;  
	}

	void Update(){
		isOff = radar.isOff;
		distanceIsOff = radar.distanceIsOff;
	}
	
	//Called once per frame, after the update  
	void LateUpdate()  
	{  
		//find out the position on the screen of this game object  
		goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);   
		
		//Could have used the following line, instead of lines 70 and 71  
		//goViewportPos = Camera.main.WorldToViewportPoint(goTransform.position);  
		goViewportPos.x = goScreenPos.x/(float)Screen.width;  
		goViewportPos.y = goScreenPos.y/(float)Screen.height;  
	}  
	
	//Draw GUIs  
	void OnGUI()  
	{  
		truePos = new Vector2(goScreenPos.x, Screen.height - goScreenPos.y);
		if (renderer.isVisible) {
						
						//Begin the GUI group centering the speech bubble at the same position of this game object. After that, apply the offset  
						GUI.BeginGroup (new Rect (goScreenPos.x, Screen.height - goScreenPos.y, bubbleWidth, bubbleHeight));  
		
						//Render the round part of the bubble  
						//GUI.Label(new Rect(0,0,200,100),"",guiSkin.customStyles[0]);  
		
						//Render the text
			string pos = ((int)Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").transform.position)).ToString ();
			if(distanceIsOff){

				pos = ((int)Random.Range(100,1000)).ToString();
			}


						GUI.Label (new Rect (0, 0, 190, 50), pos, guiSkin.label);  

						GUI.EndGroup ();
						GUI.DrawTexture (new Rect (goScreenPos.x-15f, Screen.height - goScreenPos.y-15f, bubbleWidth, bubbleHeight), lockImage);
				}
	}  
	
	//Called after camera has finished rendering the scene  

}  