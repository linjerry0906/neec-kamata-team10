//------------------------------------------------------
// 作成日：2018.5.18
// 作成者：林 佳叡
// 内容：プレイヤーがジャンプするアニメーション
//------------------------------------------------------
using UnityEngine;

public class PlayerAnimeJump : IAnimeState
{
    public void Execute(Animator animator)
    {
        animator.SetBool("isJump", true);
    }

    public void Exit(Animator animator)
    {
        animator.SetBool("isJump", false);
    }

    public bool IsEnd()
    {
        return false;
    }

    public void Update()
    {
        return;
    }
}
