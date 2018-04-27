using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseEnemy : MoveEnemy
{
    private Rigidbody rigidBody; //剛体

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        direction = Direction.LEFT;
    }


    /// <summary>
    /// 移動
    /// </summary>
    public void ChaseMove(Collider player)
    {
        float offsetPosX = OffsetPosition(player).x;                    //プレイヤーと敵との横の距離を求める
        float offsetPosY = OffsetPosition(player).y;                    //プレイヤーと敵との縦の距離を求める

        if (IsNotPlayerleave(offsetPosY))                               //Y軸に2タイル以上離れていなかったら
        {

            DirectionDetermination(offsetPosX);                         //プレイヤーのいる方向を判断する

            if (IsCloseThePlayerX(offsetPosX)) { return; }              //プレイヤーに近すぎたら移動させない
        }
        HorizontalMove();                                               //移動
    }

    /// <summary>
    /// 自動移動
    /// </summary>
    public void AutoMove()
    {

        HorizontalMove();
    }


    /// <summary>
    /// 移動方向の決定
    /// </summary>
    /// <param name="offsetPos">プレイヤーと敵の距離</param>
    /// <returns></returns>
    void DirectionDetermination(float offsetPos)
    {
        if (offsetPos > 0)               //プレイヤーが右にいたら
        {
            direction = Direction.RIGHT; //右向きにする
            return;
        }

        direction = Direction.LEFT;      //左にいるので左向きにする
    }

    /// <summary>
    /// 敵とプレイヤーとの距離を求める
    /// </summary>
    /// <returns></returns>
    private Vector3 OffsetPosition(Collider player)
    {
        Vector3 offsetPos =  player.transform.position - transform.position;

        return offsetPos;

    }

    /// <summary>
    /// 移動制限(Playerに限りなく近づいたら動かさない)
    /// </summary>
    /// <param name="differenceX">X差分</param>
    /// <returns></returns>
    private bool IsCloseThePlayerX(float differenceX)
    {
        const int limitedRangeX = 1; //横軸の移動制限範囲

        if (Mathf.Abs(differenceX) <= limitedRangeX) { return true; }   //playerに1タイル分近づくと動かさない

        return false;
    }

    /// <summary>
    /// Y軸に2タイル分離れているか
    /// </summary>
    /// <param name="differenceY">Y軸の差分</param>
    /// <returns></returns>
    private bool IsPlayerleaveY(float differenceY)
    {
        const int limitedRangeY = 2; //縦軸の移動制限範囲

        if (Mathf.Abs(differenceY) >= limitedRangeY) { return true; }   //Y方向に2タイル以上離れてる

        return false;
    }


    /// <summary>
    /// Y軸に2タイル以上離れていないか(可視化用)
    /// </summary>
    /// <param name="differenceY">プレイヤーと敵の縦座標の差分</param>
    /// <returns></returns>
    private bool IsNotPlayerleave(float differenceY)
    {
        return !(IsPlayerleaveY(differenceY));
    }


    /// <summary>
    /// プレイヤーにぶつかった時
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player") { return; }            //プレイヤーじゃなかったら実行しない。

        ObjectSize size = GetComponent<ObjectSize>();    //エネミーのサイズ

        if(IsSmall(size))                                
        {
            Destroy(gameObject);                         //小さかったら自分が死ぬ
            return;
        }

        other.GetComponent<AliveFlag>().Dead();          //小さくなかったのでプレイヤーが死ぬ
    }

    /// <summary>
    /// 小さいか
    /// </summary>
    /// <param name="size">エネミーのサイズ</param>
    /// <returns></returns>
    private bool IsSmall (ObjectSize size)
    {
        if (size.GetSize() == SizeEnum.Small_XY){ return true; }    //全体的に小さいか
        if (size.GetSize() == SizeEnum.Small_X) { return true; }    //横に縮んでいるか
        if (size.GetSize() == SizeEnum.Small_Y) { return true; }    //縦に縮んでいるか

        return false;                                               //縮んでいない。
    }
}
