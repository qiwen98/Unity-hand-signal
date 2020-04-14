using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System.Globalization;

public class SerialInput2 : MonoBehaviour
{
    private SerialPort arduinoStream;
    public string port2 = "/dev/cu.usbserial-A9UNX4QC";
    private Thread readThread;
    private string readMessage;
    bool isNewMessage;
    public string data2;
    public float dat1;
    public float dat2;
    public float time2 = 0;

    //public finger1_1rotate f11;
    
    private void ArduinoRead()
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
        if (port2 != "")
        {
            arduinoStream = new SerialPort(port2, 9600);
            arduinoStream.ReadTimeout = 10;
            try
            {
                arduinoStream.Open();
                // readThread = new Thread (new ThreadStart (ArduinoRead));
                readThread = new Thread(ArduinoRead);
                readThread.Start();
                Debug.Log("SerialPort Started.");
            }
            catch
            {
                Debug.Log("SerialPort Failed");
            }
        }
    }

    void Update()
    {
        time2 += 1 * Time.deltaTime;        
        if (time2 > 3)
        {
            if (isNewMessage)
            {
                data2 = readMessage;
                string[] datas = data2.Split(',');
                dat1 = float.Parse(datas[0]);
                dat2 = float.Parse(datas[1]);
                //f11.updatef11();
            }
            isNewMessage = false;
        }        
    }
}
