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
    int stage;
    int selectStage;
    GameObject parent;
    bool IsUnlockstage;


    void Start()
    {
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
        }
        else if (!IsUnlockstage)
        {
            GetComponent<Image>().color = Color.red;
        }
        //それ以外は明るくする
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    //ステージをセットするメソッドを書く
    public void StageSet(int currentStageCount)
    {
        stage = currentStageCount;
        transform.GetChild(1).GetComponent<BestTimeTextPrint>().SetBestTime(stage);
        IsUnlockstage = GameManager.Instance.UnlockManager().IsUnlocked((EScene)stage);
    }

    //そのintを返す(ステージセレクト用)
    public void ReturnStage()
    {
        //Debug.Log(stage + "がクリックされた");
        if (IsUnlockstage)
        {
            GameManager.Instance.SelectStage(stage);
            GameManager.Instance.GetSystemSE().PlaySystemSE(SystemSoundEnum.se_enter);
        }
        else
        {
            //ToDo SE
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
}
