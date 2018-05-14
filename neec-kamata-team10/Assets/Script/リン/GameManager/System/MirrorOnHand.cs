//------------------------------------------------------
// 作成日：2018.5.7
// 作成者：林 佳叡
// 内容：持っている鏡
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorOnHand : MonoBehaviour
{
    private readonly Vector2 relativePos = new Vector2(4, 1);
    private GameObject player;

    /// <summary>
    /// Player設定
    /// </summary>
    /// <param name="player">プレイヤー</param>
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    /// <summary>
    /// 座標更新
    /// </summary>
	public void UpdateHand()
    {
        EDirection d = player.GetComponent<Player>().GetDirection();        //向き
        float x = relativePos.x;
        if (d == EDirection.LEFT)                                           //左の場合
            x *= -1;
        Vector3 playerPos = player.transform.position;                      //Playerの位置
        Vector3 pos = playerPos + new Vector3(x, relativePos.y);
        pos.z = 0.1f;                                                       //Z = 0.1 の平面
        transform.position = pos;                                           //座標設定
    }
}
