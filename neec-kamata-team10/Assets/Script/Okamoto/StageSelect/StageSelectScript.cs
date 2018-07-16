using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScript : MonoBehaviour
{

    [SerializeField]
    Color select;
    [SerializeField]
    Color lockSelect;
    [SerializeField]
    Color lockColor;
    int stage;
    int selectStage;
    GameObject parent;
    bool IsUnlockstage = true;
    [SerializeField]
    bool findParent = true;


    void Start()
    {
        if(findParent)
            parent = gameObject.transform.parent.parent.parent.gameObject;
    }

    void Update()
    {
        //ButtonSelectから選択されたボタンを感知する
        selectStage = parent.GetComponent<ButtonSelect>().ReturnSelectStage();
        //選択されたボタンを暗くする
        if (selectStage == stage)
        {
            GetComponent<Image>().color = IsUnlockstage ? select : lockSelect;
            parent.GetComponent<StagePanelCreate>().SetBackGround(stage, IsUnlockstage);
        }
        else if (!IsUnlockstage)
        {
            GetComponent<Image>().color = lockColor;
        }
        //それ以外は明るくする
        else
        {
            GetComponent<Image>().color = Color.grey;
        }
    }

    //ステージをセットするメソッドを書く
    public void StageSet(int currentStageCount)
    {
        stage = currentStageCount;
        if (transform.childCount < 2) return;
        transform.GetChild(1).GetComponent<BestTimeTextPrint>().SetBestTime(stage);
        IsUnlockstage = GameManager.Instance.UnlockManager().IsUnlocked((EScene)stage);
    }

    //そのintを返す(ステージセレクト用)
    public void ReturnStage()
    {
        if (stage == (int)EScene.Credit)
        {
            GameManager.Instance.ChangeScene((EScene)stage);
            GameManager.Instance.GetSystemSE().PlaySystemSE(SystemSoundEnum.se_enter);
        }

        //Debug.Log(stage + "がクリックされた");
        if (IsUnlockstage)
        {
            GameManager.Instance.SelectStage(stage);
            GameManager.Instance.GetSystemSE().PlaySystemSE(SystemSoundEnum.se_enter);
        }
        else
        {
            GameManager.Instance.GetSystemSE().PlaySystemSE(SystemSoundEnum.se_error);
        }
    }

    //テキストに渡す用
    public int StageNumber()
    {
        return stage;
    }

    //ベストタイム表示
    void bestTimePrint()
    {
        GameManager.Instance.UnlockManager().ClearTime((EScene)stage);
    }

    public void SetParent(GameObject parent)
    {
        this.parent = parent;
    }
}
