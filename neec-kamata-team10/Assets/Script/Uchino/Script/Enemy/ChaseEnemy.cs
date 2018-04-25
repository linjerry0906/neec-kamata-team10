using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向き
/// </summary>
public enum Direction
{
    RIGHT =  1,
    LEFT  = -1
};


public class ChaseEnemy : Enemy
{
    [SerializeField]
    float moveSpeed = 1; //移動スピード
    Direction direction; //向き
    Rigidbody rigidBody; //剛体


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
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
    /// 任意の横方向に一定のスピードで移動
    /// </summary>
    private void HorizontalMove()
    {
        float distance = (int)direction * moveSpeed * Time.deltaTime;   //毎フレームの移動距離を計算
        PositionChangeX(distance);                                      //横に移動させる
        
    }

    /// <summary>
    /// 現在の位置にXをを足し込む
    /// </summary>
    /// <param name="X"></param>
    void PositionChangeX(float X)
    {
        //自分の位置を一旦保存 
        Vector3 newPosition =                                           
           new Vector3(transform.position.x + X, transform.position.y, transform.position.z);  

        transform.position = newPosition; //新しいPositionにする                            

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
    /// 向きを反転させる
    /// </summary>
    public void ReverseDirection()
    {
        //左向きなら右に、右向きだったら左向きにする。
        direction = (direction == Direction.LEFT) ? (Direction.RIGHT) : (Direction.LEFT);
    }
}
