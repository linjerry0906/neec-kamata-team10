using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

    public GameObject fadePanel;            //フェード用のパネル
    private FadeController fadeController;　//フェード制御クラス

	// Use this for initialization
	void Start () {
        fadeController = fadePanel.GetComponent<FadeController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// フェードアウト
    /// </summary>
    public void FadeOut()
    {
        fadeController.FadeOutStart();
    }
}
