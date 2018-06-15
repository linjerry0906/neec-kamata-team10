using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnime : MonoBehaviour {

    Animator anim;
    EnemyAliveFlag enemyAliveFlag;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyAliveFlag = GetComponent<EnemyAliveFlag>();
    }


    public void Animation(MoveEnemy enemy)
    {
        //倒れるアニメーション
        FallAnimation();
        FlipAnimation(enemy);
    }

    public void FlipAnimation(MoveEnemy enemy)
    {
        anim.SetInteger("Direction",(int)enemy.Direction);
    }

    void FallAnimation()
    {
        if (!enemyAliveFlag.IsDead()) return;
        anim.SetBool("IsDead", true);

        //倒れるアニメーションが終了したら
        if (IsFallAnimationEnds())
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
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead")) { return false; }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f) { return false; }

        return true;

    }

}
