using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour {

    /// <summary>
    /// ゴール地点のスクリプト(フラグ立てるだけ)
    /// </summary>

    //外部から変えられたくない
    private bool isGoal = false; //初期状態は念のためfalseにしておく

    void OnTriggerEnter(Collider other) //触れた瞬間
    {
        if (isGoal || !other.tag.Equals("Player")) return; //もうゴールしました or プレイヤー以外とぶつかりました -> return

        //else{
        isGoal = true; //フラグ立てる
        Debug.Log("Goal!"); //デバッグログ
        //}
    }

    public bool IsGoal { get { return isGoal; } }
}
