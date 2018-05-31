using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonToObject : MonoBehaviour {

    Vector3 defaultScale;
    Vector3 previousScale;

    private void Start()
    {
        Transform parentTransform = transform.parent.transform;
        transform.position = new Vector3(parentTransform.position.x, parentTransform.position.y
            , parentTransform.position.z);

        defaultScale = transform.parent.localScale;
        previousScale = defaultScale;

    }

    bool isMirrorColison = false;
    private void Update()
    {

        ////if (transform.parent.localScale != previousScale)
        ////{
        ////    if (previousScale != defaultScale)
        ////    {
        ////        transform.parent.localScale = previousScale;
        ////        isMirrorColison = true;
        ////    }
        ////}

        ////if(isMirrorColison)
        ////{
        ////    transform.parent.localScale = previousScale;
        ////}

        ////previousScale = transform.parent.localScale;
        //Debug.Log(transform.parent.localScale + "代入前");
        //transform.parent.gameObject.transform.localScale = new Vector3(2, 2, 2);
        //Debug.Log(transform.parent.localScale);
        //Debug.Log("defaultScale"+defaultScale);
    }



    /// <summary>
    /// 他のオブジェクトにぶつかった時
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag != "Player") { return; }                //プレイヤーじゃなかったら実行しない。

        KillOrDeath(other);                                   //衝突時の状態で敵が死ぬかプレイヤーが死ぬか判定する
    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }


    private void KillOrDeath(Collider other)
    {
        ObjectSize size = GetComponentInParent<ObjectSize>(); //エネミーのサイズ

        if (size == null)                                     //鏡に影響を受けない敵なら無条件でプレイヤーが死ぬ 
        {
            other.GetComponent<AliveFlag>().Dead();
            return;
        }

        if (IsSmall(size))                                    //エネミーが小さいか
        {
            Destroy(transform.parent.gameObject);                    //小さかったら自分が死ぬ
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
        if (size.GetSize() == SizeEnum.Small_XY){ return true; }    //全体的に小さいか
        if (size.GetSize() == SizeEnum.Small_X) { return true; }     //横に縮んでいるか
        if (size.GetSize() == SizeEnum.Small_Y) { return true; }     //縦に縮んでいるか

        return false;                                                //縮んでいない。
    }

}
