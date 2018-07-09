﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonToObject : MonoBehaviour {

    public AudioClip deadClip;
    AudioSource audioSource;

    private void Start()
    {
        Transform parentTransform = transform.parent.transform;
        transform.position = new Vector3(parentTransform.position.x, parentTransform.position.y
            , parentTransform.position.z);

        parentPosY = transform.parent.position.y;
        audioSource = GetComponentInParent<AudioSource>();
        audioSource.clip = deadClip;
    }

    private void FixedUpdate()
    {
        Positioning();
        UpdateSizeState();
    }

    float parentPosY;
    /// <summary>
    /// BigXYの時に地面に埋まらないように位置を調整
    /// </summary>
    private void Positioning()
    {
        if (!IsEnlarged()) return;

        Vector3 scale = transform.parent.localScale;
        Vector3 parentPos = transform.parent.position;
       
        transform.parent.position = 
            new Vector3(parentPos.x, parentPosY + scale.y / 2 / 2, parentPos.z);    //スケールが増えた分上に移動させる。
    }

    float previousSizeY = 0;
    void UpdateSizeState()
    {
        previousSizeY = transform.lossyScale.y;
    }

    bool IsEnlarged()
    {
        if (!(transform.lossyScale.y > previousSizeY)) return false;

        return true;
    }

    /// <summary>
    /// 他のオブジェクトにぶつかった時
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player")
            && !other.CompareTag("Splinter")) { return; }           //プレイヤーかトゲじゃなかったら実行しない。

        KillOrDeath(other);                                        //衝突時の状態で敵が死ぬかプレイヤーが死ぬか判定する
    }


    private void KillOrDeath(Collider other)
    {

        ObjectSize size = GetComponentInParent<ObjectSize>(); //エネミーのサイズ

        if (other.CompareTag("Splinter"))
        {
            audioSource.PlayOneShot(deadClip);
            GetComponentInParent<EnemyDead>().Dead();
            return;
        }

        if (size == null)                                     //鏡に影響を受けない敵なら無条件でプレイヤーが死ぬ 
        {
            other.GetComponent<AliveFlag>().Dead();
            return;
        }

        if (IsSmall(size) )                                   //エネミーが小さいときはエネミーが死ぬ  
        {
            audioSource.PlayOneShot(deadClip);
            GetComponentInParent<EnemyDead>().Dead();
            return;
        }

        other.GetComponent<AliveFlag>().Dead();               //小さくなかったのでプレイヤーが死ぬ
    }

    /// <summary>
    /// 小さいか
    /// </summary>
    /// <param name="size">エネミーのサイズ</param>
    /// <returns></returns>
    private bool IsSmall(ObjectSize size)
    {
        SizeEnum eSize = size.GetSize();
        if (eSize == SizeEnum.Small_XY){ return true; }     //全体的に小さいか
        if (eSize == SizeEnum.Small_X) { return true; }     //横に縮んでいるか
        if (eSize == SizeEnum.Small_Y) { return true; }     //縦に縮んでいるか

        return false;                                       //縮んでいない。
    }



}
