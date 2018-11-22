using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OptionsLoader : MonoBehaviour
{
    public void OpenOptionsScene()
    {
        SceneManager.LoadScene("Options");
    }

}
