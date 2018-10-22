using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minLogoTime = 1.0f; // Minimum time of that scene
    private float animationDurationTime = 10.0f;
	// Use this for initialization
	void Start () {
        // Grab the only canvasgroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Start with a white screen
        fadeGroup.alpha = 1;
        
        // Pre load the game
        //  $$

        //Get a thimestamp of the completion time
        // if loadtime is super, give it a small buffer time so we can apreciate the logo
        if(Time.time < minLogoTime)
        {
            loadTime = minLogoTime;
        }
        else
        {
            loadTime = Time.time;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (loadTime != 0 &&
            Time.time <= animationDurationTime)
        {
            fadeGroup.alpha = Mathf.Cos(Time.time * 2.5f);
        }
        else
        {
            // Out of duration seconds
            SceneManager.LoadScene("Home");
        }
        
        /*
		// Fade in
        if(Time.time < minLogoTime)
        {
            fadeGroup.alpha = 1 - (Time.time/);
            Debug.Log(1-Time.time);
        }
        // Fade out
        if (Time.time > minLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minLogoTime;
        }
        */
    }
}
