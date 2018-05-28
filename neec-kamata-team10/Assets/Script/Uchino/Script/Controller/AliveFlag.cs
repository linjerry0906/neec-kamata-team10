using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveFlag : MonoBehaviour
{
    bool isDead = false;

    /// <summary>
    /// 死亡フラグを立てる
    /// </summary>
    public void Dead()
    {
        isDead = true;
        GameManager.Instance.GetStageManager().EndStage();
        GameManager.Instance.GetStageManager().SetClear(false);
    }

    /// <summary>
    /// 死亡しているか
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return isDead;
    }

    /// <summary>
    /// デバッグ
    /// </summary>
    public void Dump()
    {
        Debug.Log(isDead);
    }


}
