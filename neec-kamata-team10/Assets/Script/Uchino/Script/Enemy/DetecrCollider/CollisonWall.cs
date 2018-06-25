using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonWall : MonoBehaviour {

    bool isColisonWall = false;
    private void OnTriggerEnter(Collider other)
    {
        SetOnColisonWall(other,true);

        if (!IsBlockTag(other)) return; //Blockじゃなかったら
            
        NormalEnemy nomalEnemy = GetComponentInParent<NormalEnemy>();
        if (nomalEnemy != null)
        {
            gameObject.GetComponentInParent<NormalEnemy>().ReverseDirection();  //プレイヤー以外のブロックに当たったら反転
        }

        ThrowingEnemy throwingEnemy = GetComponentInParent<ThrowingEnemy>();
        if (throwingEnemy!= null)
        {
            gameObject.GetComponentInParent<ThrowingEnemy>().ReverseDirection();  //プレイヤー以外のブロックに当たったら反転
        }
    }

    private void SetOnColisonWall(Collider other,bool isColisonWall)
    {
        if (!IsBlockTag(other)) return;

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

    public bool IsBlockTag(Collider other)
    {
        if (other.tag == "stage_block") return true;
        if (other.tag == "magic_block") return true;

        return false;
    }
    


}
