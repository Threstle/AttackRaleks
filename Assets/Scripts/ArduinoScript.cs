using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
public class ArduinoScript : MonoBehaviour 
{
	//Setup parameters to connect to Arduino
	public static SerialPort sp = new SerialPort("/dev/tty.usbmodem1421", 9600, Parity.None, 8, StopBits.One);
	public static SerialPort sp1 = new SerialPort("/dev/tty.usbmodem9991411", 9600, Parity.None, 8, StopBits.One);
	public static SerialPort sp2 = new SerialPort("/dev/tty.usbmodem1431", 9600, Parity.None, 8, StopBits.One);
	public static SerialPort sp3 = new SerialPort("/dev/tty.usbmodem1a1311", 9600, Parity.None, 8, StopBits.One);
	public static SerialPort sp4 = new SerialPort("/dev/tty.usbmodem1411", 9600, Parity.None, 8, StopBits.One);
	public static string strIn;
	public string message;
	public string message1;
	public string message2;
	public string message3;
	public string message4;
	public string messageToSend;
	public string messageToSend1;
	public string messageToSend2;
	public string messageToSend3;
	public string messageToSend4;
	public List<int> messageList;

	private bool isId;
	private string data;

	//variables vaisseau
	public int speed;
	public int joyY;
	public int joyX;
	public bool wantShoot;
	public bool wantBomb;
	public bool isLaser;
	public string keypad;

	public bool armes_repair;
	public bool armes_screw;
	public int armes_lightCaptor;
	public string armes_keypad;
	
	public bool generator_repair;
	public bool generator_screw;
	public int generator_en_reactors;
	public int generator_en_armes;
	public int generator_en_radar;

	public bool radar_repair;
	public bool radar_screw;
	public int  radar_freq;

	public bool reacteur_repair;
	public bool reacteur_screw;
	public bool reacteur_left;
	public bool reacteur_right;
	
	public int reacteur_armor;
	public bool reacteur_left_fire;
	public bool reacteur_right_fire;

	Thread myThread;
	// Use this for initialization
	void Start () 
	{

		OpenConnection();

	}
	
	void Update()
	{
	
		if (sp4.IsOpen) {

	
			//message = sp.ReadLine ();
			//message1 = sp1.ReadLine();
			//message2 = sp2.ReadLine();
			//message3 = sp3.ReadLine();
			message4 = sp4.ReadLine();
			//Debug.Log(message);
			//Debug.Log(message1);

			Debug.Log(message4);
		
			analyseMessage();

			composeMessage();

//			sp.Write(messageToSend);
//			sp1.Write(messageToSend1);
//			sp2.Write(messageToSend2);
			//sp3.Write(messageToSend3);
			sp4.Write(messageToSend4);

			message = "";
			message1 = "";
			message2 = "";
			message3 = "";
			message4 = "";
		}
		
	}

	//Fonctions appel


	
	
	
	//Function connecting to Arduino

	//Deparsing Function
	public void analyseMessage(){
	

		//COCKPIT
		char[] charTab = message.ToCharArray ();
//		if (charTab.Length > 0) {
//			speed = rateTo100(convertChar(charTab[0]))*2;
//			isLaser = convertToBool(charTab[1]);
//			keypad =  charTab[2].ToString();
//			joyY = rateTo100(convertChar(charTab[3]))-50;
//			joyX = rateTo100(convertChar(charTab[4]))-50;
//			wantShoot = convertToBool(charTab[6]);
//			wantBomb = convertToBool(charTab[7]);
//		}

		//ARMEMENT
//		charTab = message1.ToCharArray ();
//		if (charTab.Length > 0) {
//			armes_repair = convertToBool(charTab[0]);
//			armes_screw = convertToBool(charTab[1]);
//			armes_lightCaptor = rateTo100(convertChar(charTab[2]));
//			armes_keypad = charTab[3].ToString();
//		}

//		//GENERATEUR
//		charTab = message2.ToCharArray ();
//
//		if (charTab.Length > 0) {
//			generator_repair = convertToBool(charTab[0]);
//			generator_screw = convertToBool(charTab[1]);
//			generator_en_reactors = convertChar(charTab[2]);
//			generator_en_armes = convertChar(charTab[3]);
//			generator_en_radar = convertChar(charTab[4]);
//		}

		//RADAR
//		charTab = message3.ToCharArray ();
//		if (charTab.Length > 0) {
//			radar_repair = convertToBool(charTab[0]);
//			radar_screw = convertToBool(charTab[1]);
//			radar_freq = convertChar (charTab[2]);
//		}

		//REACTEUR
		charTab = message4.ToCharArray ();
		if (charTab.Length > 0) {
			reacteur_repair = convertToBool(charTab[0]);
			reacteur_screw = convertToBool(charTab[1]);
			reacteur_left = convertToBool(charTab[2]);
			reacteur_right = convertToBool(charTab[3]);
		}

		message = "";
		message1 = "";
		message2 = "";
		message3 = "";
		message4 = "";
	}

	public void composeMessage(){
		messageToSend4 = "";
		messageToSend4 += reacteur_armor;
	}

	public int rateTo100(int value){
		return (int)(((float)value / 35.0) * 100.0);
	}

	public bool convertToBool(char ch){
		if (ch.ToString() == "1")
						return true;
				else
						return false;
	}


	public void openConnectionSerial(SerialPort spA){
		Debug.Log (spA);
		if (spA != null) 
		{
			if (spA.IsOpen) 
			{
				spA.Close();
				//message = "Closing port, because it was already open!";
			}
			else 
			{
				spA.Open();  // opens the connection
				spA.ReadTimeout = 1000;  // sets the timeout value before reporting error
				
			}
		}
		else 
		{
			if (spA.IsOpen)
			{
				print("Port is already open");
			}
			else 
			{
				print("Port == null");
			}
		}
	}
	public void OpenConnection() 
	{
//		openConnectionSerial (sp);
//		openConnectionSerial (sp1);
//		openConnectionSerial (sp2);
//		openConnectionSerial (sp3);
		openConnectionSerial (sp4);
	}
	
	void OnApplicationQuit() 
	{
		sp.Close();
		sp1.Close ();
		sp2.Close ();
		sp3.Close ();
		sp4.Close ();
	}

	//DECRYPT METHODS

	public char convertNumber(int number){
		char[] numberConvertTable = {
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
			'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
			'u', 'v', 'w', 'x', 'y', 'z'
		};

		return number > 35 ? '+' : numberConvertTable[(number < 0 ? 0 : number)];

	}

	public int convertChar(char ch){
		char[] numberConvertTable = {
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
			'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
			'u', 'v', 'w', 'x', 'y', 'z'
		};

		return Array.IndexOf<char> (numberConvertTable, ch);
		//return number > 35 ? '+' : numberConvertTable[(number < 0 ? 0 : number)];
		
	}
}