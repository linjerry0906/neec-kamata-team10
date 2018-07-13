using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRange : MonoBehaviour
{
    ChaseEnemy chaseEnemy;
    private void Start()
    {
        chaseEnemy = GetComponentInParent<ChaseEnemy>(); //ChasEnemyスクリプトの取得
        groundInfo = GetComponentInParent<GroundInfo>(); //地面の情報を取得
    }
    /// <summary>
    /// プレイヤーを感知する（メイン）
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        isColison = true;                                            //プレイヤーを検知した

        if (WhetherChaisingOrNot(other))                             //追いかけるか否か
        {
            chaseEnemy.ChaseMove(other);                             //追いかける
            return;
        }

        chaseEnemy.AutoMove();                                       //自動で移動させる
    }

    bool isColison = false; //DeciveEnemy用
    private void OnTriggerExit(Collider other)
    {
        isColison = false;
    }

    /// <summary>
    /// 動いてるか
    /// </summary>
    /// <returns></returns>
    public bool IsMove()
    {
        return isColison;
    }

    /// <summary>
    /// 追いかけるか行ったり来たりするか判断して移動させる
    /// </summary>
    /// <param name="player"></param>
    private bool WhetherChaisingOrNot(Collider player)
    {
        Vector3 playerPosition = player.transform.position;          //プレイヤーの位置を取得

        if (!IsOutOfRangeGround(playerPosition))                     //プレイヤーが同じ地面の上にいるか
        {
            return true;                                             //追尾する                       
        }

        return false;                                                //追尾しない
    }

    GroundInfo groundInfo;
    /// <summary>
    /// プレイヤーがエネミーのいる地面いるかいないか
    /// </summary>
    /// <param name="playerPosition">プレイヤーの位置</param>
    /// <returns></returns>
    private bool IsOutOfRangeGround(Vector3 playerPosition)
    {
        if(groundInfo.IsSetLeft())                                          //左地面端の位置がセットされているか
        {
            if (playerPosition.x < groundInfo.LeftEdgeX)
            { return true; }                                                //プレイヤーが地面の左端より左にいる
        }
        if(groundInfo.IsSetRight())                                         //右地面端の位置がセットされているか
        {
            if (playerPosition.x > groundInfo.RightEdgeX)
            { return true; }                                                //プレイヤーが地面の右端より右にいる
        }

        return false;                                                       //プレイヤーは地面の上にいる
    }

    

}
