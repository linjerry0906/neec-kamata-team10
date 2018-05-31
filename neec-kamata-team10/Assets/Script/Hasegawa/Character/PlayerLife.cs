using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {
    [SerializeField]
    private int lifeCnt;　　　//現在の残機
    [SerializeField]          
    private int lifeLimit;    //残機の上限

    private bool isGameOver = false;

    //残機の設定
    public void SetLifeCnt(int lifeCnt)
    {
        this.lifeCnt = lifeCnt;
    }

    //残機の減少
    public void Dead()
    {
        lifeCnt--;
        if (lifeCnt < 0) isGameOver = true;
    }

    //残機の増加
    public void LifeIncrease()
    {
        lifeCnt++;
        if (lifeLimit < lifeCnt) lifeCnt = lifeLimit;
    }

    //GameOverフラグの取得
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
