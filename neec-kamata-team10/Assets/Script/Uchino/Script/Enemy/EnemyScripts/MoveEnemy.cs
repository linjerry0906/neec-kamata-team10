﻿using System.Collections;
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
    protected float moveSpeed = 1; //移動スピード
    protected Direction direction; //向き
    protected Animator anim;
    protected DetectEmpty detectEmptyFront;

    private void Awake()
    {
       GroundInfo groundInfo = gameObject
        .AddComponent(typeof(GroundInfo)) as GroundInfo;   //実体を取得
        anim = GetComponent<Animator>();

    }
    public void DirectionInit()
    {
        direction = Direction.LEFT;

        //デフォルトの向きがLeftなのでLeftを取得
        detectEmptyFront = transform.GetChild(0).GetChild(0).GetComponent<DetectEmpty>();
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


    protected float time = 0.0f;                  //遅延処理用タイマー
    protected readonly float twoFrame = 0.034f;   //遅れさせる時間
    /// <summary>
    /// 地面端の設定
    /// </summary>
    protected virtual void SetGroundEdge()
    {
        time += Time.deltaTime;         //時間更新

        //1フレーム遅れて実行
        if (time >= twoFrame)
        {
            time = 0.0f;

            //ここから修正

            //地面に当たってなかったら
            if (!detectEmptyFront.IsCollison())        //前面のColliderが地面から離れたら
            {
                ReverseDirection();                    //移動方向を反転させる
            }

            detectEmptyFront.MyUpdate();     //地面との接触判定のために必要
        }
    }

    /// <summary>
    /// 左右の画像反転処理(SimpleEnemy用)
    /// </summary>
    protected void FlipAnimation_Simple()
    {
        //localScaleの値を1か-1か判断する
        Vector3 offsetscale = transform.localScale;

        //offsetSceleの取得方法を見直し
        offsetscale.x = ((int)Direction * offsetscale.x > 0) ? -1 : 1;

        //左右反転
        transform.localScale = new Vector3(transform.localScale.x * offsetscale.x,
            offsetscale.y, offsetscale.z);
    }

    /// <summary>
    /// ChaseEnemy用
    /// </summary>
    protected void FlipAnimation()
    {
         anim.SetInteger("Direction", (int)Direction);
    }

}
