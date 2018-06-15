﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour {

    int stage;
    private ICharacterController controller;

    [SerializeField]
    private GameObject parentObject;
    private StageSelectScript s;
    private int allButton;
    private int panelButton;
    private int constrain;
    private int allPage;
    private int page;

    // Use this for initialization
    void Start () {
        stage = 1;
        allButton = 0;
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
            for (int i = 0; i < allPage; i++) 
            {
                allButton += transform.GetChild(i).GetChild(0).childCount;
            }
            CheckPanelButton();
            return;
        }

        //Debug.Log(panelButton);
        //Debug.Log(constrain);
        //Debug.Log(index);
        //選択ステージが端っこじゃないとき
        if (!(stage == allButton))
        {
            if (!(stage % constrain == 0))
            {
                if (controller.MoveSelectionRight()) stage++;
            }
        }
        if(!(stage % constrain == 1))
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
        //終わりのページじゃないとき
        if (!(page == allPage))
        {
            //下にシフトできるとき
            if (stage + constrain <= (panelButton * page))
            {
                if (controller.MoveSelectionDown()) stage += constrain;
            }
            //上にシフトできるとき
            if (stage - constrain > ((panelButton * page) - panelButton))
            {
                if (controller.MoveSelectionUp()) stage -= constrain;
            }
        }
        //終わりのページの時
        else
        {
            //下にシフトできるとき
            if (stage + constrain <= allButton)
            {
                if (controller.MoveSelectionDown()) stage += constrain;
            }
            //上にシフトできるとき
            if (stage - constrain > allButton - panelButton)
            {
                if (controller.MoveSelectionUp()) stage -= constrain;
            }
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
