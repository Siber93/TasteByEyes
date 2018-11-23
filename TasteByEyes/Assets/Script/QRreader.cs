using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class QRreader : MonoBehaviour {

    /// <summary>
    /// Indirizzo Ip del dispositivo di controllo remoto
    /// </summary>
    public static String Ip_Control_Address = "";

    private WebCamTexture camTexture;
    private Rect screenRect;

    /// <summary>
    /// True  => E' stato cliccato il bottone per resettare l'IP
    /// False => Nessuna richiesta di lettua QR pendente
    /// </summary>
    private bool Read = false;

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
                    Debug.Log("DECODED TEXT FROM QR: " + result.Text);
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
        }
    }
}
