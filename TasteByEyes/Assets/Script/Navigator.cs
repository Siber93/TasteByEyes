using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour {

    /// <summary>
    /// Distrugge tutte le istanze della scena indicata
    /// </summary>
    public void UnloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }


    /// <summary>
    /// Consente di passare alla visualizzazione 3D caricando il file specificato
    /// </summary>
    /// <param name="fileToLoad"> File da caricare nel 3D</param>
    public void Load3DScene(string fileToLoad)
    {
        ObjectLoader.FileToLoad = fileToLoad;
        SceneManager.LoadScene("3DView");
    }

    /// <summary>
    /// Carica una scena generica indicata attraverso il suo nome
    /// </summary>
    /// <param name="scene">Nome della scena di istanziare</param>
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
