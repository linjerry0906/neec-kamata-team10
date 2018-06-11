using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    bool isDead = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Animation();
    }

    public void Dead()
    {
        isDead = true;
    }

    public bool IsDead()
    {
        return isDead;
    }

    void Animation()
    {
        //倒れるアニメーション
        FallAnimation();
    }

    /// <summary>
    /// 倒れるアニメーション
    /// </summary>
    void FallAnimation()
    {
        if (!isDead) return;
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
