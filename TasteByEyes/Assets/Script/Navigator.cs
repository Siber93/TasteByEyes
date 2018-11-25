using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class Navigator : MonoBehaviour, IVirtualButtonEventHandler {



    public GameObject BackBtn;

    void Start()
    {
        if(BackBtn != null)
        {
            BackBtn.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        }
    }



    /// <summary>
    /// Consente di passare alla visualizzazione 3D caricando il file specificato
    /// </summary>
    /// <param name="fileToLoad"> File da caricare nel 3D</param>
    public void Load3DScene(string fileToLoad)
    {
        if (QRreader.Ip_Control_Address != "")
        {
            ObjectLoader.FileToLoad = fileToLoad;
            LoadScene("3DView");
        }
        else
        {
            // Avverto che l'Ip non è settato
            MessageBox.ShowMessageBox("IP not set", "Remote control device's IP has not been set. Please do it by QR code reading on settings menu.");
        }
    }

    /// <summary>
    /// Carica una scena generica indicata attraverso il suo nome
    /// </summary>
    /// <param name="scene">Nome della scena di istanziare</param>
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        LoadScene("Home");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        //throw new System.NotImplementedException();
    }
}
