using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Visualize3D : MonoBehaviour {

    /// <summary>
    /// Consente di passare alla visualizzazione 3D caricando il file specificato
    /// </summary>
    /// <param name="fileToLoad"> File da caricare nel 3D</param>
    public void ChangeScene(string fileToLoad)
    {
        ObjectLoader.FileToLoad = fileToLoad;
        SceneManager.LoadScene("3DView");        
    }
    
}
