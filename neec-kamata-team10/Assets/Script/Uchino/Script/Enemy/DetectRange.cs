using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRange : MonoBehaviour {

    /// <summary>
    /// プレイヤーを感知する（メイン）
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }            //プレイヤーじゃなかったら実行しない
  
        ChaseEnemy chaseEnemy = GetComponentInParent<ChaseEnemy>();  //ChaseEnemyを取得


        if (WhetherChaisingOrNot(other))                             //追いかけるか否か
        {
            chaseEnemy.ChaseMove(other);                             //追いかける
            return;
        }

        chaseEnemy.AutoMove();                                       //自動で移動させる
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
            return true;                                             
        }

        return false;                                                
    }

    /// <summary>
    /// プレイヤーがエネミーのいる地面いるかいないか
    /// </summary>
    /// <param name="playerPosition">プレイヤーの位置</param>
    /// <returns></returns>
    private bool IsOutOfRangeGround(Vector3 playerPosition)
    {
        GroundInfo groundInfo = GetComponentInParent<GroundInfo>();      //地面の情報を取得s


        if (playerPosition.x < groundInfo.LeftEdgeX)  { return true; }   //プレイヤーが地面の左端より左にいる
        if (playerPosition.x > groundInfo.RightEdgeX) { return true; }   //プレイヤーが地面の右端より右にいる

        return false;                                                    //プレイヤーは地面の上にいる
    }

    

}
