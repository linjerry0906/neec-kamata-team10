using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legCollider : MonoBehaviour {

    void OnTriggerStay(Collider t)
    {
        Player p = transform.parent.GetComponent<Player>();

        if (t.gameObject.CompareTag("stage_block"))
        {
            //Debug.Log("stageに衝突");
            //プレイヤーのジャンプフラグをfalseにする
            p.SetPlayerState(EPlayerState.Move);
            p.SetIsJump(false);
        }
        if (t.gameObject.CompareTag("ivy_upSideCollider"))
        {
            p.SetPlayerState(EPlayerState.Move);
            p.SetIsJump(false);
        }
        if (t.gameObject.CompareTag("magic_block"))
        {
            p.SetPlayerState(EPlayerState.Move);
            p.SetIsJump(false);
        }
        if (t.gameObject.CompareTag("seasaw"))
        {
            p.SetPlayerState(EPlayerState.Move);
            p.SetIsJump(false);
        }

        //Debug.Log(t);
    }

    void OnTriggerExit(Collider t)
    {
        Player p = transform.parent.GetComponent<Player>();
        p.SetPlayerState(EPlayerState.Jump);
        p.SetIsJump(true);
        //Debug.Log(t+"離れた");
    }
}
