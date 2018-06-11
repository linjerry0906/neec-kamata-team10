using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legCollider : MonoBehaviour
{

    void OnTriggerStay(Collider t)
    {
        Player p = transform.parent.GetComponent<Player>();
        string tag = t.gameObject.tag;

        if (tag == "stage_block" || tag == "magic_block" || tag == "seasaw")
        {
            p.SetPlayerState(EPlayerState.Move);
            p.SetIsJump(false);
        }

        if (tag == "seasaw")
        {
            p.SetIsMountSeesaw(true);
        }

        //if (t.gameObject.CompareTag("stage_block"))
        //{
        //    //Debug.Log("stageに衝突");
        //    //プレイヤーのジャンプフラグをfalseにする
        //    p.SetPlayerState(EPlayerState.Move);
        //    p.SetIsJump(false);
        //}
        //if (t.gameObject.CompareTag("ivy_upSideCollider"))
        //{
        //    p.SetPlayerState(EPlayerState.Move);
        //    p.SetIsJump(false);
        //}
        //if (t.gameObject.CompareTag("magic_block"))
        //{
        //    p.SetPlayerState(EPlayerState.Move);
        //    p.SetIsJump(false);
        //}
        //if (t.gameObject.CompareTag("seasaw"))
        //{
        //    p.SetPlayerState(EPlayerState.Move);
        //    p.SetIsJump(false);
        //}

        //Debug.Log(t);
    }

    void OnTriggerExit(Collider t)
    {
        string tag = t.gameObject.tag;
        if (tag == "stage_block" || tag == "magic_block" || tag == "seasaw")
        {
            Player p = transform.parent.GetComponent<Player>();
            p.SetPlayerState(EPlayerState.Jump);
            p.SetIsJump(true);
            //Debug.Log(t + "を離れた");
        }
        if (tag == "seasaw")
        {
            Player p = transform.parent.GetComponent<Player>();
            p.SetIsMountSeesaw(false);
            //Debug.Log("しーそーから離れた");
        }
    }
}
