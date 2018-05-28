using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonWall : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "mirror") { return; }
        Debug.Log(other.tag);
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

    ObjectSize objectSize;
    BoxCollider boxCollider;
    private void Start()
    {
        objectSize = GetComponentInParent<ObjectSize>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        boxCollider.center = Vector3.zero;

        if(objectSize.GetSize() != SizeEnum.Big_XY) { return; }
            boxCollider.center = new Vector3(0, 0.22f, 0);              //コライダーの位置調整

    }

}
