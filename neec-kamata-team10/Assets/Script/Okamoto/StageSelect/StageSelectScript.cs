using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScript : MonoBehaviour {

    [SerializeField]
    Color select;
    int stage;
    int selectStage;
    GameObject parent;
    

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
            GetComponent<Image>().color = select;
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
    }

    //そのintを返す(ステージセレクト用)
    public void ReturnStage()
    {
        //Debug.Log(stage + "がクリックされた");
        GameManager.Instance.SelectStage(stage);
    }

    //テキストに渡す用
    public int StageNumber()
    {
        return stage;
    }
}
