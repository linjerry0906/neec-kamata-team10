using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

    private ICharacterController controller;                      //コントローラー

    // Use this for initialization
    void Start () {
        controller = GameManager.Instance.GetController();
    }
	
	// Update is called once per frame
	void Update () {
        if (controller.Jump()) {
            Debug.Log("change");
            GameManager.Instance.ChangeScene(EScene.StageSelect); }
	}
}
