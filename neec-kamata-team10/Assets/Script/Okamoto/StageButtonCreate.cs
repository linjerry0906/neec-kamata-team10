using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtonCreate : MonoBehaviour {

    [SerializeField]
    private GameObject originButton;

    [SerializeField]
    private int buttonCount;

    List<Button> buttons;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < buttonCount; i++)
        {
            //GUI.Button(new Rect(i, 20, 100, 100), "Button");
            GameObject b = Instantiate(originButton, transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    
}
