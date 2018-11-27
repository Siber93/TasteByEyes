using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ObjectLoader : MonoBehaviour {


    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    #endregion


    // Oggetto all'interno del quale verrà inserito il 3d
    public GameObject ContainerView;

    public static string FileToLoad = "";

    GameObject obj = null;

    public static Queue<short> RotationQueue = new Queue<short>();


    short smoothing_aim = 0;
    float current_smoothing = 0;
    const float SMOOTHING_FACTOR = 0.6f;

    // Use this for initialization
    void Start () {
        // Pulisco la coda per sicurezza
        RotationQueue.Clear();
        if (ContainerView != null &&
            FileToLoad != "")
        {
            /*UnityEngine.Object pPrefab = Resources.Load("Assets/Pizza/13917_Pepperoni_v2_l2"); // note: not .prefab!
            GameObject pNewObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity);*/

            // Ottengo l'oggetto da caricare
            obj = Instantiate(Resources.Load(FileToLoad) as GameObject);

            // Lo assegno al container
            obj.transform.SetParent(ContainerView.transform);

            // Lo scalo
            obj.transform.localScale = new Vector3(0.1f,0.1f,0.1f);

            // Lo ruoto
            obj.transform.Rotate(new Vector3(1, 0, 0), -90);

            ConnectToTcpServer();

        }
	}
	
	// Update is called once per frame
	void Update () {
        if(RotationQueue.Count>0)
        {
            // Cambio obiettivo nel processo di smoothing
            smoothing_aim = RotationQueue.Dequeue();

            // Resetto lo smoothing;
            current_smoothing = 0;
        }

        float last_value = current_smoothing;

        current_smoothing = (float)(SMOOTHING_FACTOR * smoothing_aim + (1 - SMOOTHING_FACTOR) * current_smoothing);

        // Rotate the object
        obj.transform.Rotate(new Vector3(0, 0, 1), current_smoothing - last_value);
    }




    /// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            socketConnection = new TcpClient(QRreader.Ip_Control_Address, 8052);
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        short val = BitConverter.ToInt16(bytes, 0);
                        val *= 2 ;
                        // Do the rotatating operation			
                        RotationQueue.Enqueue(val);
                        Debug.Log("Received: " + val.ToString());
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    /// <summary> 	
    /// Send message to server using socket connection. 	
    /// </summary> 	
    private void SendMessage()
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                string clientMessage = "This is a message from one of your clients.";
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

}

