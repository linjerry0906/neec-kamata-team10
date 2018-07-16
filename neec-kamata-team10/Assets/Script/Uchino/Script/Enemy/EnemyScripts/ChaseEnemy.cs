using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseEnemy : MoveEnemy
{

    // Use this for initialization
    void Start()
    {
        colisonWall = GetComponentInChildren<CollisonWall>();
        direction = Direction.LEFT;

        detectEmptyLeft  = transform.GetChild(1).GetChild(0).GetComponent<DetectEmpty>();
        detectEmptyRight = transform.GetChild(1).GetChild(1).GetComponent<DetectEmpty>();
    }

    private void Update()
    {
        SetGroundEdge();                                 //地面端をセット
        FlipAnimation();                                 //アニメーションで反転   
    }

    //地面の端を検知するオブジェクト群
    DetectEmpty detectEmptyLeft;                         //左の地面端
    DetectEmpty detectEmptyRight;                        //右の地面端
    /// <summary>
    /// 地面端の設定
    /// </summary>
    protected override void SetGroundEdge()
    {
        time += Time.deltaTime;                                    //タイマーの更新

        //2フレーム遅れて実行
        if (time >= twoFrame)
        {
            time = 0.0f;                                           //タイマーの初期化

            //地面に当たってなかったら
            if (!detectEmptyLeft.IsCollison())
            {
                detectEmptyLeft.SetGroundEdge(Direction.LEFT);
                SetDirection(Direction.RIGHT);                     //移動方向を反転させる
            }
            if (!detectEmptyRight.IsCollison())                    
            {
                detectEmptyLeft.SetGroundEdge(Direction.RIGHT);
                SetDirection(Direction.LEFT);                      //移動方向を反転させる
            }

            detectEmptyLeft.MyUpdate();                            //地面との接触判定に必要
            detectEmptyRight.MyUpdate();                           //地面との接触判定に必要
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    public void ChaseMove(Collider player)
    {
        Vector3 offSetPosition = OffsetPosition(player);        //プレイヤーと敵との距離を求める
        float offsetPosX = offSetPosition.x;                    //プレイヤーと敵との横の距離を求める
        float offsetPosY = offSetPosition.y;                    //プレイヤーと敵との縦の距離を求める

        if (IsNotPlayerleave(offsetPosY))                       //Y軸に2タイル以上離れていなかったら
        {
            DirectionDetermination(offSetPosition.x);           //プレイヤーのいる方向を判断する

            if (IsCloseThePlayerX(offsetPosX)) { return; }      //プレイヤーに近すぎたら移動させない
        }

        //OnColisonWallPositioning();                             //先に位置を補正しておく。
        HorizontalMove();                                       //移動

    }

    /// <summary>
    /// 敵とプレイヤーとの距離を求める
    /// </summary>
    /// <returns></returns>
    private Vector3 OffsetPosition(Collider player)
    {
        Vector3 offsetPos = player.transform.position - transform.position;

        return offsetPos;

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
        direction = (offsetPos > 0) ? Direction.RIGHT : Direction.LEFT;
    }

    const float limitedRangeX = 0.4f;    //横軸の移動制限範囲
    /// <summary>
    /// 移動制限(Playerに限りなく近づいたら動かさない)
    /// </summary>
    /// <param name="differenceX">X差分</param>
    /// <returns></returns>
    private bool IsCloseThePlayerX(float differenceX)
    {
        if (Mathf.Abs(differenceX) <= limitedRangeX) { return true; }   //playerに1タイル分近づくと動かさない

        return false;
    }

    const int limitedRangeY = 2;         //縦軸の移動制限範囲
    /// <summary>
    /// Y軸に2タイル分離れているか
    /// </summary>
    /// <param name="differenceY">Y軸の差分</param>
    /// <returns></returns>
    private bool IsPlayerleaveY(float differenceY)
    {
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

    CollisonWall colisonWall;                               //壁衝突検知用オブジェクト
    bool isSaveCollisonPosition = false;                    //ぶつかったときのX座標を保存したか
    float onCollisonWallPositionX;                          //ぶつかったときのX座標

    /// <summary>
    /// ぶつかったときのX軸の場所を保存
    /// </summary>
    private void SaveOnCollisonPosition()
    {
        //壁から離れたらぶつかった場所をリセット
        if(!colisonWall.IsWallColison())
        {
            isSaveCollisonPosition = false;
        }

        //壁にぶつかった場所を保存
        if (isSaveCollisonPosition) return;
        onCollisonWallPositionX = transform.position.x;

        isSaveCollisonPosition = true;        
    }

    /// <summary>
    /// 壁にぶつかったときにめり込まないように位置を修正
    /// </summary>
    private void OnColisonWallPositioning()
    {
        //ぶつかった場所を保存
        SaveOnCollisonPosition();

        //壁に当たってなかったら実行しない
        if (!colisonWall.IsWallColison()) { return; }

        Vector3 myPosition = transform.position;
        transform.position = new Vector3(onCollisonWallPositionX, myPosition.y, myPosition.z);

    }
}
