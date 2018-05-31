//------------------------------------------------------
// 作成日：2018.5.18
// 作成者：林 佳叡
// 内容：プレイヤーが鏡を設置するアニメーション
//------------------------------------------------------
using UnityEngine;

public class PlayerAnimeAction : IAnimeState
{
    private bool endFlag = true;

    public void Execute(Animator animator)
    {
        animator.SetBool("isAction", true);
        endFlag = false;
    }

    public void Exit(Animator animator)
    {
        animator.SetBool("isAction", false);
    }

    public bool IsEnd()
    {
        return endFlag;
    }

    public void Update()
    {
        endFlag = true;
    }
}
