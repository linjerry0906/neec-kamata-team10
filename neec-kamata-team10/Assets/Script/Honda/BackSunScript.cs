using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSunScript : MonoBehaviour {

    private GameObject mainCamera; //MainCameraのObject
    private Vector3 vector;        //Camとの相対位置

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Main Camera");  

        if (mainCamera == null) Destroy(gameObject); //MainCameraが見つからない場合壊せ

        vector = this.transform.position - mainCamera.transform.position; //初期位置保存s
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = mainCamera.transform.position + vector;
	}
}
