using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour {

    int stage;
    private ICharacterController controller;

    [SerializeField]
    private GameObject parentObject;
    private StagePanelCreate panelPerButton;

    // Use this for initialization
    void Start () {
        stage = 1;
        controller = GameManager.Instance.GetController();
        panelPerButton = parentObject.GetComponent<StagePanelCreate>();
    }
	
	// Update is called once per frame
	void Update () {
        //コントローラー出来次第書き込みます
        //if () stage++;
        //if () stage--;
        //if () stage += panelPerButton.ReturnPanelPerButton() / 4;
        //if () stage -= panelPerButton.ReturnPanelPerButton() / 4;
    }
}
