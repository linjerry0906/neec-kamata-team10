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
        if (isDead)
            return;

        isDead = true;
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = GetComponent<SEManager>().GetSE(1);
        audio.Play();
        GetComponent<PlayerAnime>().ChangeState(EPlayerState.Dead);
        //GameManager.Instance.GetStageManager().EndStage();
        //GameManager.Instance.GetStageManager().SetClear(false);
    }

    /// <summary>
    /// 死亡しているか
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return isDead;
    }



}
