using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour {

    int stage;
    private ICharacterController controller;

    [SerializeField]
    private GameObject parentObject;
    private int panelButton;
    private int constrain;
    private int page;

    // Use this for initialization
    void Start () {
        stage = 1;
        page = 1;
        constrain = 0;
        controller = GameManager.Instance.GetController();
    }
	
	// Update is called once per frame
	void Update () {
        if(constrain == 0)
        {
            constrain = transform.GetChild(0).GetChild(0).GetComponent<GridLayoutGroup>().constraintCount;
            panelButton = GetComponent<StagePanelCreate>().ReturnPanel(page);
            return;
        }

        int index = (panelButton) / constrain;
        Debug.Log(panelButton);
        //Debug.Log(constrain);
        //Debug.Log(index);
        //選択ステージが端っこじゃないとき
        if (!(stage % index == 0))
        {
            if (controller.MoveSelectionRight()) stage++;
        }
        if(!(stage % index == 1))
        {
            if (controller.MoveSelectionLeft()) stage--;
        }

        //ページ送り
        if (controller.SwitchToTheRight())
        {
            page++;
            stage = stage + panelButton;
        }
        if (controller.SwitchToTheLeft())
        {
            page--;
            stage = stage - panelButton;
        }

        //下にシフトできるとき
        if (stage + index <= (panelButton * page))
        {
            if (controller.MoveSelectionDown()) stage += panelButton / constrain;
        }
        //上にシフトできるとき
        if (stage - index > ((panelButton * page) - panelButton))
        {
            if (controller.MoveSelectionUp()) stage -= panelButton / constrain;
        }
        Debug.Log(stage);
    }
}
