using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonWall : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") { return; }

        gameObject.GetComponentInParent<ThrowingEnemy>().ReverseDirection();  //プレイヤー以外のブロックに当たったら反転
    }
}
