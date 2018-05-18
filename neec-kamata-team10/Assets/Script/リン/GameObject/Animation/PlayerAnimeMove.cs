//------------------------------------------------------
// 作成日：2018.5.18
// 作成者：林 佳叡
// 内容：プレイヤーが移動するアニメーション
//------------------------------------------------------
using UnityEngine;

public class PlayerAnimeMove : IAnimeState
{
    public void Execute(Animator animator)
    {
        animator.SetBool("isWalk", true);
    }

    public void Exit(Animator animator)
    {
        animator.SetBool("isWalk", false);
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
