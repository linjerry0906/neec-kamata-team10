using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonWall : MonoBehaviour {

    bool isColisonWall = false; //壁に当たったか

    //7.9 本田 加筆
    private bool isUpdateDone = false; //リサイズで二重に判定が起こるとバグったので

    void Update()
    {
        isUpdateDone = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsReversObjectTag(other.tag)) return;  //壁のオブジェクトでなければ実行しない
        isColisonWall = true;                       //壁に当たった

        if (isUpdateDone) return;                   //リサイズ時でも二重に判定はさせない
        isUpdateDone = true;

        ReversDirection();                          //壁にぶつかったので、移動方向を反転させる。
    }

    /// <summary>
    /// SimpleEnemyの移動方向を反転させる。
    /// </summary>
    private void ReversDirection()
    {
        NormalEnemy nomalEnemy = GetComponentInParent<NormalEnemy>();
        if (nomalEnemy != null)
        {
            gameObject.GetComponentInParent<NormalEnemy>().ReverseDirection();  //プレイヤー以外のブロックに当たったら反転
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //壁に当たってない
        isColisonWall = false;
    }

    /// <summary>
    /// 壁に当たったか
    /// </summary>
    /// <returns></returns>
    public bool IsWallColison()
    {
        return isColisonWall;
    }

    /// <summary>
    /// 壁または敵に衝突したか
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public bool IsReversObjectTag(string tag)
    {
        if (tag.Equals("stage_block"))  return true;
        if (tag.Equals("magic_block"))  return true;
        if (tag.Equals("appear_block")) return true;
        if (tag.Equals("Enemy"))        return true;         

        return false;
    }
    


}
