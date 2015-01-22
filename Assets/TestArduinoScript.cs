using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class TestArduinoScript : MonoBehaviour {
	SerialPort serial = new SerialPort();
	string distance;
	// Use this for initialization
	void Start () {
		
		serial.ReadBufferSize = 8192;
		serial.WriteBufferSize = 128;
		
		serial.PortName = "/dev/tty.usbmodem1411";
		serial.BaudRate = 9600;
		serial.Parity = Parity.None;
		serial.StopBits = StopBits.One;


		try{
			serial.Open();
			serial.DataReceived += DataReceivedHandler;
		}
		catch(Exception e){
			Debug.Log("Could not open serial port: " + e.Message);
			
		}
		
	}
	
	private void DataReceivedHandler(
		object sender,
		SerialDataReceivedEventArgs e)
	{
		SerialPort sp = (SerialPort)sender;
		string msg = sp.ReadLine();
		Debug.Log(msg);
	}
}
