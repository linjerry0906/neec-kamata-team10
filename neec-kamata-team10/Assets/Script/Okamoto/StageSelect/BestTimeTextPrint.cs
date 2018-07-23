using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTimeTextPrint : MonoBehaviour {

    int stage;
   

	// Use this for initialization
	public void SetBestTime (int stage)
    {
        
        DateTime scoreTime = GameManager.Instance.UnlockManager().ClearTime((EScene)stage);
        //string time = String.Format("{0}:{1}:{2}", scoreTime.Minute, scoreTime.Second, scoreTime.Millisecond);
        string time =  scoreTime.ToString("mm:ss:fff");
        GetComponent<Text>().text = time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
