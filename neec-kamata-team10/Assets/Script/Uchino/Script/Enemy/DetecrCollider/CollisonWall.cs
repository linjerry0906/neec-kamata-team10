using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonWall : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { return; }

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

}
