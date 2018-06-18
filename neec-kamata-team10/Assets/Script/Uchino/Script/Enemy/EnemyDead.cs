using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour {

    Animator anim;

    bool isDead = false;

    public void Dead()
    {
        isDead = true;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Update()
    {
        Animation();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Animation()
    {
        //倒れるアニメーション
        FallAnimation();
        //FlipAnimation();
    }

    public void FlipAnimation()
    {
        anim.SetInteger("Direction", (int)transform.localScale.x);
    }

    void FallAnimation()
    {
        if (!IsDead()) return;
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
