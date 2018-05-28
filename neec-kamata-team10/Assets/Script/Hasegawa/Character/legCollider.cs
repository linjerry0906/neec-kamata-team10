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
            transform.parent.GetComponent<Player>().SetPlayerState(EPlayerState.Move);
            transform.parent.GetComponent<Player>().SetIsJump(false);
        }
        if (t.gameObject.CompareTag("ivy_upSideCollider"))
        {
            transform.parent.GetComponent<Player>().SetPlayerState(EPlayerState.Move);
            transform.parent.GetComponent<Player>().SetIsJump(false);
        }
        if (t.gameObject.CompareTag("magic_block"))
        {
            transform.parent.GetComponent<Player>().SetPlayerState(EPlayerState.Move);
            transform.parent.GetComponent<Player>().SetIsJump(false);
        }
        if (t.gameObject.CompareTag("seasaw"))
        {
            transform.parent.GetComponent<Player>().SetPlayerState(EPlayerState.Move);
            transform.parent.GetComponent<Player>().SetIsJump(false);
        }
    }

    void OnTriggerExit(Collider t)
    {
        transform.parent.GetComponent<Player>().SetPlayerState(EPlayerState.Jump);
        transform.parent.GetComponent<Player>().SetIsJump(true);
    }
}
