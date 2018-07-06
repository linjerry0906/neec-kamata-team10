using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour {

    Animator anim;

    bool isDead = false;

    //7.5 本田 Enemyの死亡時にパーティクルが出るように追加
    [SerializeField]
    private ParticleSystem deadParticle;

    public void Dead()
    {
        isDead = true;

        if(deadParticle != null)
        {
            deadParticle.Play();
        }
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

        deadParticle.Stop();
    }


    public void Animation()
    {
        //倒れるアニメーション
        FallAnimation();
    }

    public void FlipAnimation()
    {
        anim.SetInteger("Direction", (int)transform.localScale.x);
    }

    void FallAnimation()
    {
        if (!IsDead()) return;
        anim.SetBool("IsDead", true);

        //死ぬときはオブジェクトに影響させないように重さを0にする。
        Rigidbody rgdb = GetComponent<Rigidbody>();
        rgdb.mass = 0f;

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
