using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legCollider : MonoBehaviour {

    void OnTriggerStay(Collider t)
    {
        if (t.gameObject.CompareTag("stage_block"))
        {
            //Debug.Log("stageに衝突");
            //プレイヤーのジャンプフラグをfalseにする
            transform.parent.GetComponent<Player>().SetIsJump(false);
        }
    }

    void OnTriggerExit(Collider t)
    {
        //Debug.Log("stageに衝突していない");
        //プレイヤーのジャンプフラグをtrueにする
        transform.parent.GetComponent<Player>().SetIsJump(true);
    }
}
