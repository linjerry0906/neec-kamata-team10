//------------------------------------------------------
// 作成日：2018.6.11
// 作成者：林 佳叡
// 内容：プレイヤー死んだ時のアニメーション
//------------------------------------------------------
using System;
using UnityEngine;

public class PlayerAnimDead : IAnimeState
{
    private Animator animator;

    public void Execute(Animator animator)
    {
        this.animator = animator;
        animator.SetTrigger("isDead");
    }

    public void Exit(Animator animator)
    {
        return;
    }

    public bool IsEnd()
    {
        return false;
    }

    void IAnimeState.Update()
    {
        if (!animator.GetBehaviour<AnimeScriptDead>().IsEnd())
            return;
        //終了したらステージ再開
        GameManager.Instance.GetStageManager().EndStage();
        GameManager.Instance.GetStageManager().SetClear(false);
    }
}
