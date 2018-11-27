using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QRreader : MonoBehaviour {

    /// <summary>
    /// Indirizzo Ip del dispositivo di controllo remoto
    /// </summary>
    public static String Ip_Control_Address = "";

    private WebCamTexture camTexture;
    private Rect screenRect;

    GameObject label;
    Text txt;

    /// <summary>
    /// True  => E' stato cliccato il bottone per resettare l'IP
    /// False => Nessuna richiesta di lettua QR pendente
    /// </summary>
    private bool Read = false;


    void Start()
    {
        label = GameObject.Find("OptionsContainer/IPtxt");
        txt = label.GetComponent(typeof(Text)) as Text;
        //Ip_Control_Address = "192.168.1.10";
        txt.text = "HW IP: " + Ip_Control_Address;
    }

    /// <summary>
    /// Handler del click sul bottone di rilettura indirizzo IP
    /// </summary>
    public void OpenQRReader()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
        if (camTexture != null)
        {
            camTexture.Play();
        }
        Read = true;
        Debug.Log("QR Reading");
    }

    void OnGUI()
    {
        if (Read)
        {
            // drawing the camera on screen
            GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
            // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                // decode the current frame
                var result = barcodeReader.Decode(camTexture.GetPixels32(),
                  camTexture.width, camTexture.height);
                if (result != null)
                {
                    Read = false;
                    Ip_Control_Address = result.Text;
                    txt.text = "HW IP: " + result.Text;
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
        }
    }
}
