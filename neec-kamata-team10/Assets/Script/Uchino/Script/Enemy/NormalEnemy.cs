using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MoveEnemy
{
    Animator anim;
    EnemyAliveFlag aliveFlag;

    void Start()
    {
        DirectionInit();

        anim = GetComponent<Animator>();
        aliveFlag = GetComponent<EnemyAliveFlag>();
    }

    void Update()
    {
        SetGroundEdge();                                    //地面端の設定
        HorizontalMove();                                   //行ったり来たり

        Animation();                                        //アニメーション
    }

    void Animation()
    {
        //左右反転のアニメーション
        FlipAnimation();
        //倒れるアニメーション
        FallAnimation();
    }

    /// <summary>
    /// 倒れるアニメーション
    /// </summary>
    void FallAnimation()
    {
        if (!aliveFlag.IsDead()) return;
        anim.SetBool("isDead", true);

        //倒れるアニメーションが終了したら
        if(IsFallAnimationEnds())
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 倒れるアニメーションが終了しているか
    /// </summary>
    /// <returns></returns>
    bool IsFallAnimationEnds()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("SimpleEnemy_Fall")) { return false; }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f)      { return false; }

        return true;

    }

}


