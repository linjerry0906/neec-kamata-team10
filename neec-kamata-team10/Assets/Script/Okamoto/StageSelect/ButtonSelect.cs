using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour {

    int stage;
    private ICharacterController controller;

    [SerializeField]
    private GameObject parentObject;
    [SerializeField]
    private GameObject creditButton;
    private StageSelectScript s;
    private GameObject selectingButton;
    private int allButton;
    private int panelButton;
    private int constrain;
    private int allPage;
    private int page;

    private bool creditFlag;

    // Use this for initialization
    void Start () {
        stage = 1;
        allButton = 0;
        allPage = 0;
        page = 1;
        constrain = 0;
        controller = GameManager.Instance.GetController();
        creditButton.GetComponent<StageSelectScript>().StageSet((int)EScene.Credit);
        creditButton.GetComponent<StageSelectScript>().SetParent(gameObject);

        creditFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Startでできなかった初期値設定
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

        if(controller.IsFade())
            return;
        StageSelect();
        PageFeed();
        StageSelectEnter();

        //Debug.Log(panelButton);
        //Debug.Log(constrain);
        //Debug.Log(index);
        //Debug.Log(stage);
        //Debug.Log(creditFlag);
    }

    void CheckPanelButton()
    {
        panelButton = GetComponent<StagePanelCreate>().ReturnPanel(page);
    }

    //ページ送り
    void PageFeed()
    {
        //ページ送り
        //今のpageがallPageより少ないとき
        if (page < allPage)
        {
            //右ページ送りボタンが押されたら
            if (controller.SwitchToTheRight())
            {
                page++;
                //ページ送りした時にstageがallButtonを超えないように設定
                //allButtonより少なかったらpanelButtonを足す
                if (stage + panelButton < allButton)
                {
                    stage = stage + panelButton;
                }
                //それ以外はstageをallButtonと同じにする
                else if(stage + panelButton >= allButton)
                {
                    stage = allButton;
                }
            }
        }
        //pageが1より上でかつ左ページ送りボタンが押されたら
        if (page > 1 && controller.SwitchToTheLeft())
        {
            page--;
            stage = stage - panelButton;
        }
    }

    //ステージを選ぶ
    void StageSelect()
    {
        if(creditFlag)
        {
            if (controller.MoveSelectionUp() || controller.MoveSelectionRight()) creditFlag = false;
            return;
        }
        //選択ステージが端っこじゃないとき
        if (!(stage == allButton))
        {
            if (!(stage % constrain == 0))
            {
                if (controller.MoveSelectionRight())
                {
                    stage++;
                    return;
                }
            }
        }
        if (!(stage % constrain == 1))
        {
            if (controller.MoveSelectionLeft())
            {
                stage--;
                return;
            }
        }


        //終わりのページじゃないとき
        if (!(page == allPage))
        {
            //左下の時クレジットボタンに移動
            if (page * panelButton - constrain + 1 == stage)
            {
                if (controller.MoveSelectionDown() || controller.MoveSelectionLeft()) creditFlag = true;
            }

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
            int temple = allButton % constrain;
            temple = temple == 0 ? constrain : temple;
            //左下の時クレジットボタンに移動
            if (allButton - (temple) + 1 == stage)
            {
                if (controller.MoveSelectionDown() || controller.MoveSelectionLeft()) creditFlag = true;
            }

            //下にシフトできるとき
            if (stage + constrain <= allButton)
            {
                if (controller.MoveSelectionDown()) stage += constrain;
            }
            
            //上にシフトできるとき
            if (stage - constrain > (page - 1) * panelButton )
            {
                if (controller.MoveSelectionUp()) stage -= constrain;
            }
        }

        

    }

    //ステージ決定
    void StageSelectEnter()
    {
        if (controller.Jump())
        {
            if (creditFlag)
            {
                creditButton.GetComponent<StageSelectScript>().ReturnStage();
                return;
            }
            //ステージがpanelButtonと同じ時用
            int index = page * transform.GetComponent<StagePanelCreate>().ReturnPanelPerButton() - 1;

            //stage % panelButton が0じゃないとき
            if (stage % panelButton != 0) s = transform.GetChild(page - 1).GetChild(0).GetChild(stage % panelButton - 1).GetComponent<StageSelectScript>();
            
            //0の時
            else s = transform.GetChild(page - 1).GetChild(0).GetChild(index).GetComponent<StageSelectScript>();

            s.ReturnStage();
        }
    }

    //選択されたボタンをintで返す(StageSelectScript用)
    public int ReturnSelectStage()
    {
        if (creditFlag) return (int)EScene.Credit;
        return stage;
    }

    
}
