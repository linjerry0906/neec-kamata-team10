using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonToObject : MonoBehaviour {

    ObjectSize objectSize;
    AudioSource audioSource;

    private void Start()
    {
        Transform parentTransform = transform.parent.transform;
        transform.position = new Vector3(parentTransform.position.x, parentTransform.position.y
            , parentTransform.position.z);

        objectSize = GetComponentInParent<ObjectSize>();
        parentPosY = transform.parent.position.y;
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        Positioning();
    }

    float parentPosY;
    /// <summary>
    /// BigXYの時に地面に埋まらないように位置を調整
    /// </summary>
    private void Positioning()
    {
        if (!IsBigSize()) return;

        Vector3 scale = transform.parent.localScale;
        Vector3 parentPos = transform.parent.position;
       
        transform.parent.position = 
            new Vector3(parentPos.x, parentPosY + scale.y / 2 /2, parentPos.z);    //スケールが増えた分上に移動させる。

    }

    bool IsBigSize()
    {
        if (objectSize.GetSize() == SizeEnum.Big_XY) return true;
        if (objectSize.GetSize() == SizeEnum.Big_Y)  return true;

        return false;
    }

    /// <summary>
    /// 他のオブジェクトにぶつかった時
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {

        if (other.tag != "Player" 
            && other.tag != "Splinter") { return; }           //プレイヤーかトゲじゃなかったら実行しない。


        KillOrDeath(other);                                   //衝突時の状態で敵が死ぬかプレイヤーが死ぬか判定する
    }

    private void KillOrDeath(Collider other)
    {
        if(other.tag == "Splinter")                           //相手はトゲ？
        {
            Destroy(transform.parent.gameObject);             //無条件で自分が死ぬ
            return;
        }

        ObjectSize size = GetComponentInParent<ObjectSize>(); //エネミーのサイズ

        if (size == null)                                     //鏡に影響を受けない敵なら無条件でプレイヤーが死ぬ 
        {
            other.GetComponent<AliveFlag>().Dead();
            return;
        }

        if (IsSmall(size))                                    //エネミーが小さいか
        {
            audioSource.Play();
            GetComponentInParent<EnemyAliveFlag>().Dead();
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
