using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonWall : MonoBehaviour {

    bool isColisonWall = false;
    private void OnTriggerEnter(Collider other)
    {
        SetOnColisonWall(other,true);

        if (!IsBlockTag(other) && !other.CompareTag("Enemy")) return; 
        
        NormalEnemy nomalEnemy = GetComponentInParent<NormalEnemy>();
        if(nomalEnemy == null) { return; }

        gameObject.GetComponentInParent<NormalEnemy>().ReverseDirection();  //プレイヤー以外のブロックに当たったら反転
    }

    GameObject collisonWallObject;
    private void SetOnColisonWall(Collider other,bool isColisonWall)
    {
        if (!IsBlockTag(other)) return;

        this.isColisonWall = isColisonWall;
        collisonWallObject = other.gameObject;
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
        if (other.CompareTag("stage_block")) return true;
        if (other.CompareTag("magic_block")) return true;

        return false;
    }
    
    public BoxCollider BoxCollider
    {
        get { return GetComponent<BoxCollider>(); }
    }

    public GameObject CollisonWallObject
    {
        get { return collisonWallObject; }
    }

}
