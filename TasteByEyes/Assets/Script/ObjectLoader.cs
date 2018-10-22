using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoader : MonoBehaviour {

    // Oggetto all'interno del quale verrà inserito il 3d
    public GameObject ContainerView;

    public static string FileToLoad = ""; 

	// Use this for initialization
	void Start () {
        if(ContainerView != null &&
            FileToLoad != "")
        {
            /*UnityEngine.Object pPrefab = Resources.Load("Assets/Pizza/13917_Pepperoni_v2_l2"); // note: not .prefab!
            GameObject pNewObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity);*/

            // Ottengo l'oggetto da caricare
            GameObject obj = Instantiate(Resources.Load(FileToLoad) as GameObject);

            // Lo assegno al container
            obj.transform.SetParent(ContainerView.transform);

            // Lo scalo
            obj.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
