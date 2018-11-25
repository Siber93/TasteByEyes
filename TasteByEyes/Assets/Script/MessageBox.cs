using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{

    private static GameObject _messageBoxObject;


    [Header("VARIABLES")]
    public GameObject MessageBoxObject;

    
    void Start()
    {
        _messageBoxObject = MessageBoxObject;
        CloseMessageBox();
    }


    public void CloseMessageBox()
    {
        if (_messageBoxObject != null)
        {
            _messageBoxObject.SetActive(false);
        }
    }

    public static void ShowMessageBox(string title, string message)
    {
        if (_messageBoxObject != null)
        {
            _messageBoxObject.SetActive(true);
            GameObject TitObj = GameObject.Find("AlertBackground/Image/Image/Titolo");
            GameObject TxtObj = GameObject.Find("AlertBackground/Image/Image/Messaggio");
            Text tit = TitObj.GetComponent(typeof(Text)) as Text;
            Text txt = TxtObj.GetComponent(typeof(Text)) as Text;
            tit.text = title;
            txt.text = message;
        }
    }


}