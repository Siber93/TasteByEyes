using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;


	// Use this for initialization
	void Start () {
        // Grab the only canvasgroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Start with a white screen
        fadeGroup.alpha = 1;
    }
	
	// Update is called once per frame
	void Update () {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
	}
}
