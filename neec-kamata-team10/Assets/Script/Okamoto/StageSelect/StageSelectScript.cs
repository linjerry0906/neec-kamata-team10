using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour {

    int stage;

    //ステージをセットするメソッドを書く
    public void StageSet(int currentStageCount)
    {
        stage = currentStageCount;
    }
    //そのintを返す
    public void ReturnStage()
    {
        //Debug.Log(stage + "がクリックされた");
        GameManager.Instance.SelectStage(stage);
    }
}
