using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour {

    /// <summary>
    /// ゴール地点のスクリプト(フラグ立てるだけ)
    /// </summary>

    [SerializeField]
    private EScene unlockStage = EScene.Stage1;
    private StageManager stageManager;

    //外部から変えられたくない
    private bool isGoal = false; //初期状態は念のためfalseにしておく

    private void Start()
    {
        stageManager = GameManager.Instance.GetStageManager();
    }

    void OnTriggerEnter(Collider other) //触れた瞬間
    {
        if (isGoal || !other.CompareTag("Player")) return; //もうゴールしました or プレイヤー以外とぶつかりました -> return

        //else{
        isGoal = true; //フラグ立てる
        //Debug.Log("Goal!"); //デバッグログ
        stageManager.EndStage();
        stageManager.SetClear(isGoal, unlockStage);
        //}
    }
}
