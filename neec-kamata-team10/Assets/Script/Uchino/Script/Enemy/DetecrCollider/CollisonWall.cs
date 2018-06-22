using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonWall : MonoBehaviour {

    bool isColisonWall = false;
    private void OnTriggerEnter(Collider other)
    {
        SetOnColisonWall(other,true);
        if (other.tag == "Player" || other.tag == "mirror") { return; }

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
        if ((other.tag != "stage_block")) return;

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


    


}
