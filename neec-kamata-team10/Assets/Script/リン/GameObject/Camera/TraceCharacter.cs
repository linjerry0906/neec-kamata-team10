//------------------------------------------------------
// 作成日：2018.4.13
// 作成者：林 佳叡
// 内容：キャラクターを追尾するモード
//------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceCharacter : CameraMode
{
    private GameObject camera;               //カメラ
    private GameObject target;               //注目先
    private Vector3 relativePos;             //相対位置
    private float radius;                    //移動しない円の半径
    private float speed;                     //移動速度

    //ここから 2018.6.15 本田のフィールド変更部分
    private float respawnSpeed;              //Playerリスポーン時の速度
    private float traceMinValueY;            //追跡するオブジェクトY座標がこれより下に行ったら止める
    private bool isRespawnTrace;             //リスポーンしたときか？
    //ここまで

    //2018.6.15 本田 カメラ仕様変更で改造 変更部分はデフォルト値を設定して変更前に戻せるようにした
    public TraceCharacter(GameObject camera, GameObject target, Vector3 relativePos, float speed, float radius, float respawnSpeed = 7f, float traceMinValueY = -100f)
    {
        this.camera = camera;
        this.target = target;
        this.relativePos = relativePos;
        this.speed = speed;
        this.radius = radius;

        //ここから本田が追記
        this.respawnSpeed = respawnSpeed;
        this.traceMinValueY = traceMinValueY;
        isRespawnTrace = false;
        //ここまで
    }

    /// <summary>
    /// 追尾
    /// </summary>
    public void Trace()
    {
        if(!target)                           //注目先がない場合はTraceしない
            return;

        Vector3 dest = target.transform.position + relativePos;                         //目的地
        Vector3 dir = dest - camera.transform.position;                                 //方向ベクトル

        //6.15 本田追記 Y座標補正(ターゲットYが一定値未満でY移動量0)
        if (target.transform.position.y <= traceMinValueY) dir.y = 0;
        //ここまで

        float length = dir.magnitude;                                                   //距離
        if (length < radius)                                                            //範囲内は移動しない
        {
            //6.15 本田 boolの更新は追記
            Debug.Log("length < radius");
            isRespawnTrace = false;                                                     //targetに追いついたので高速移動は終了
            return;
        }

        //6.15 本田追記 状況に応じて速度を変更
        float moveSpeed = (isRespawnTrace) ? respawnSpeed : speed;

        dir /= length;                                                                  //正規化
        camera.transform.position += dir * (length - radius) * Time.deltaTime * moveSpeed;  //移動させる
        //camera.transform.position += dir * (length - radius) * Time.deltaTime * speed;  //移動させる
    }

    /// <summary>
    /// ターゲットを設定
    /// </summary>
    /// <param name="target">ターゲット</param>
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    //6.15 本田 メソッドの追加
    public void SetRespawnTrace(bool isRespawnTrace = true)
    {
        this.isRespawnTrace = isRespawnTrace;
    }
}
