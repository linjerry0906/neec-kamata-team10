//------------------------------------------------------
// 作成日：2018.5.18
// 作成者：林 佳叡
// 内容：プレイヤー死んだ時のアニメーション
//------------------------------------------------------
using System;
using UnityEngine;

public class PlayerAnimDead : IAnimeState
{
    public void Execute(Animator animator)
    {
        animator.SetTrigger("isDead");
        GameManager.Instance.GetController().SetFadeFlag(true);
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
        return;
    }
}
