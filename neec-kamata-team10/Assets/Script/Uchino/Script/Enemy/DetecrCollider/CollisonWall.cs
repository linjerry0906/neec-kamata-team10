using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonWall : MonoBehaviour {

    bool isColisonWall = false;

    //7.9 本田 加筆
    private bool isUpdateDone = false; //リサイズで二重に判定が起こるとバグったので

    void Update()
    {
        isUpdateDone = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        SetOnColisonWall(other,true);

        if (!IsReversObjectTag(other) && other.tag != "Enemy") return;

        if (isUpdateDone) return; //リサイズ時でも二重に判定はさせない

        isUpdateDone = true;

        NormalEnemy nomalEnemy = GetComponentInParent<NormalEnemy>();
        if (nomalEnemy != null)
        {
            gameObject.GetComponentInParent<NormalEnemy>().ReverseDirection();  //プレイヤー以外のブロックに当たったら反転
        }
    }

    private void SetOnColisonWall(Collider other,bool isColisonWall)
    {
        if (!IsReversObjectTag(other)) return;

        this.isColisonWall = isColisonWall;

    }

    private void OnTriggerExit(Collider other)
    {
        SetOnColisonWall(other, false);
    }

    public bool IsWallColison()
    {
        return isColisonWall;
    }

    public bool IsReversObjectTag(Collider other)
    {
        if (other.tag == "stage_block") return true;
        if (other.tag == "magic_block") return true;
        if (other.tag == "appear_block") return true;

        return false;
    }
    


}
