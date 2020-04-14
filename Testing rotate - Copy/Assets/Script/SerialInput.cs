using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System.Globalization;

public class SerialInput : MonoBehaviour
{
    private SerialPort arduinoStream;
    public string port = "/dev/cu.usbmodem143101";
    private Thread readThread;
    private string readMessage;
    bool isNewMessage;
    public string data;
    public float datx;
    public float daty;
    public float datz;
    public float time = 0;
    
    private void ArduinoRead ()
    {
        while (arduinoStream.IsOpen)
        {            
            try
            {           
                readMessage = arduinoStream.ReadLine();
                isNewMessage = true;                              
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);

            }
        }
    }

    private void OnApplicationQuit()
    {
        if (arduinoStream != null)
        {
            if (arduinoStream.IsOpen)
            {
                arduinoStream.Close();
            }
        }
    }
     
    void Start()
    {
        if (port != "")
        {
            arduinoStream = new SerialPort (port, 9600);
            arduinoStream.ReadTimeout = 10;
            try
            {
                arduinoStream.Open ();
               // readThread = new Thread (new ThreadStart (ArduinoRead));
                readThread = new Thread(ArduinoRead);
                readThread.Start ();
                Debug.Log ("SerialPort Started.");
            }
            catch
            {
                Debug.Log("SerialPort Failed");
            }
        }        
    }

    void Update()
    {
        time += 1 * Time.deltaTime;
        if (time>3)
        {
            if (isNewMessage)
            {
                data = readMessage;
                string[] datas = data.Split(',');
                datx = float.Parse(datas[0]);
                daty = float.Parse(datas[1]);
                datz = float.Parse(datas[2]);                
            }            
            isNewMessage = false;
        }      
    }       
}
