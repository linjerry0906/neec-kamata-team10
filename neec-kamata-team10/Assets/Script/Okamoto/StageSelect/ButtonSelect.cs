using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour {

    int stage;
    private ICharacterController controller;

    [SerializeField]
    private GameObject parentObject;
    private StageSelectScript s;
    private int panelButton;
    private int constrain;
    private int allPage;
    private int page;

    // Use this for initialization
    void Start () {
        stage = 1;
        allPage = 0;
        page = 1;
        constrain = 0;
        controller = GameManager.Instance.GetController();
    }
	
	// Update is called once per frame
	void Update () {
        if(constrain == 0)
        {
            constrain = transform.GetChild(0).GetChild(0).GetComponent<GridLayoutGroup>().constraintCount;
            allPage = transform.childCount;
            CheckPanelButton();
            return;
        }

        int index =constrain;
        //Debug.Log(panelButton);
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
        if (page < allPage)
        {
            if (controller.SwitchToTheRight())
            {
                page++;
                CheckPanelButton();
                stage = stage + panelButton;
            }
        }

        if (page > 1)
        {
            if (controller.SwitchToTheLeft())
            {
                page--;
                stage = stage - panelButton;
                CheckPanelButton();
            }
        }

        //下にシフトできるとき
        if (stage + index <= (panelButton * page))
        {
            if (controller.MoveSelectionDown()) stage += constrain;
        }
        //上にシフトできるとき
        if (stage - index > ((panelButton * page) - panelButton))
        {
            if (controller.MoveSelectionUp()) stage -= constrain;
        }
        Debug.Log(stage);

        if(controller.Jump())
        {
            s = transform.GetChild(page - 1).GetChild(0).GetChild(stage - 1).GetComponent<StageSelectScript>();
            s.ReturnStage();
        }
    }

    void CheckPanelButton()
    {
        panelButton = GetComponent<StagePanelCreate>().ReturnPanel(page);
    }
}
