using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class VuforiaSettingsManager : MonoBehaviour {

    public bool GMode = false;


    /// <summary>
    /// Abilita o disabilita la modalità AR con occhiali
    /// </summary>
    /// <param name="enabled"></param>
	public void SetGlassesMode(bool enabled)
    {
        GMode = enabled;
        if(GMode)
        {
            DigitalEyewearARController.Instance.SetEyewearType(DigitalEyewearARController.EyewearType.VideoSeeThrough);
            DigitalEyewearARController.Instance.SetStereoCameraConfiguration(DigitalEyewearARController.StereoFramework.Vuforia);
        }
        else
        {
            DigitalEyewearARController.Instance.SetEyewearType(DigitalEyewearARController.EyewearType.OpticalSeeThrough);
            DigitalEyewearARController.Instance.SetSeeThroughConfiguration(DigitalEyewearARController.SeeThroughConfiguration.Vuforia);
        }
    }
    
}
