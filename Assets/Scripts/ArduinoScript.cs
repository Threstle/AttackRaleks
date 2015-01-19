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
	public static string strIn;
	public string message;
	public string messageToSend;
	public List<int> messageList;

	private bool isId;
	private string data;

	//variables vaisseau
	public int speed;
	public int joyY;
	public int joyX;
	public bool wantShootLaser;
	Thread myThread;
	// Use this for initialization
	void Start () 
	{

		OpenConnection();

	}
	
	void Update()
	{
	
		if (sp.IsOpen) {

	
			message = sp.ReadLine ();
			analyseMessage();
			composeMessage();
			sp.Write(messageToSend);
		//	sp.WriteLine("634358");
			/*
			try
			{

			
				char ch = System.Convert.ToChar(sp.ReadByte());
				string str = ch.ToString();

				Debug.Log(sp.ReadLine());

				if(str != "+"){

					message+=str;
				}
				else{

					analyseMessage();
				}

			/*	Debug.Log(str);
				if(!isId){

					data = str;
					isId = false;
				}
				else{

					switch (str)
					{
					case "f":
						fuelPower = data;
						break;
					case "h":

						break;
					default:

						break;
					}
					isId = true;
				}

			}
			catch (System.Exception)
			{Debug.Log("TO");}

		*/
	
		}
		
	}

	//Fonctions appel


	
	
	
	//Function connecting to Arduino

	//Deparsing Function
	public void analyseMessage(){
	
		char[] charTab = message.ToCharArray ();
		if (charTab.Length > 0) {
			speed = rateTo100(convertChar(charTab[0]));
			wantShootLaser = convertToBool(charTab[1]);
			joyY = rateTo100(convertChar(charTab[3]))-50;
			joyX = rateTo100(convertChar(charTab[4]))-50;
		}
		//Debug.Log (rateTo100(convertChar(charTab[0])));


		message = "";
	}

	public void composeMessage(){
		//messageToSend = "";
		//messageToSend += speed;
		//Debug.Log ("MESSAGE ENVOYE : " + messageToSend);
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

	public void OpenConnection() 
	{
		if (sp != null) 
		{
			if (sp.IsOpen) 
			{
				sp.Close();
				//message = "Closing port, because it was already open!";
			}
			else 
			{
				sp.Open();  // opens the connection
				sp.ReadTimeout = 1000;  // sets the timeout value before reporting error
			
			}
		}
		else 
		{
			if (sp.IsOpen)
			{
				print("Port is already open");
			}
			else 
			{
				print("Port == null");
			}
		}
	}
	
	void OnApplicationQuit() 
	{
		sp.Close();
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