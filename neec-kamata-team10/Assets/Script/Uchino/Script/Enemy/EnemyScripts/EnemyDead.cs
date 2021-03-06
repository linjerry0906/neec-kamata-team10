﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour {

    Animator anim;
    bool isDead = false;
    [SerializeField]
    private GameObject particle;


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
        GameObject clone = Instantiate(particle, transform.localPosition, Quaternion.identity);
        clone.GetComponent<ParticleSystem>().Play();
    }

    public bool IsDead()
    {
        return isDead;
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
