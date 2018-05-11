using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向き
/// </summary>
public enum Direction
{
    RIGHT = 1,
    LEFT = -1
};


public class MoveEnemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1; //移動スピード
    protected Direction direction; //向き

    public void DirectionInit()
    {
        direction = Direction.LEFT;
    }

    /// <summary>
    /// 向きをセット
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(Direction direction)
    {
        this.direction = direction;
    }

    /// <summary>
    /// 向きを反転させる
    /// </summary>
    public void ReverseDirection()
    {
        //左向きなら右に、右向きだったら左向きにする。
        direction = (direction == Direction.LEFT) ? (Direction.RIGHT) : (Direction.LEFT);
    }

    /// <summary>
    /// 任意の横方向に一定のスピードで移動
    /// </summary>
    protected void HorizontalMove()
    {
        //Debug.Log(direction);
        float distance = (int)direction * moveSpeed * Time.deltaTime;   //毎フレームの移動距離を計算
        PositionChangeX(distance);                                      //横に移動させる

    }

    /// <summary>
    /// 現在の位置にXをを足し込む
    /// </summary>
    /// <param name="X"></param>
    protected void PositionChangeX(float X)
    {
        //自分の位置を一旦保存 
        Vector3 newPosition =
           new Vector3(transform.position.x + X, transform.position.y, transform.position.z);

        transform.position = newPosition; //新しいPositionにする                            
    }

    /// <summary>
    /// 向きを得る
    /// </summary>
    public Direction Direction
    {
        get { return direction; }
    }
}
