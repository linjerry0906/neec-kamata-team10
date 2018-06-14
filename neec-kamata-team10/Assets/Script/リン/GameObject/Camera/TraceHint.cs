//------------------------------------------------------
// 作成日：2018.4.13
// 作成者：林 佳叡
// 内容：ヒントを注目するモード
//------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceHint : CameraMode
{
    private GameObject target;                  //注目先
    private GameObject cameraObj;               //カメラゲームオブジェクト
    private Camera camera;                      //カメラコンポーネント
    private Vector3 velocity = Vector3.zero;    //Velocity
    private float dampTime;                     //ダンプタイム

    //ここから 2018.6.15 本田のフィールド変更部分
    private float respawnDampTime;              //Playerリスポーン時のDamp
    private float traceMinValueY;            //追跡するオブジェクトY座標がこれより下に行ったら止める
    private bool isRespawnTrace;             //リスポーンしたときか？
    //ここまで

    public TraceHint(GameObject target, GameObject cameraObj, float dampTime, float respawnDampTime = 1f, float traceMinValueY = -100f)
    {
        this.target = target;
        this.cameraObj = cameraObj;
        camera = cameraObj.GetComponent<Camera>();
        this.dampTime = dampTime;

        //ここから本田が追記
        this.respawnDampTime = respawnDampTime;
        this.traceMinValueY = traceMinValueY;
        isRespawnTrace = false;
        //ここまで
    }

    /// <summary>
    /// 追尾
    /// </summary>
    public void Trace()
    {
        if (!target)                            //注目先がない場合はTraceしない
            return;

        Vector3 targetPos = target.transform.position;                                                  //注目先の位置

        //6.15 本田 ターゲットのYを見て補正
        if (targetPos.y < traceMinValueY) targetPos.y = traceMinValueY;
        //ここだけ先に補正

        Vector3 point = camera.WorldToViewportPoint(targetPos);                                         //画面上の位置に変換
        Vector3 direct = targetPos - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));     //同じ深度で方向を計算する

        //6.15 本田 移動量一定未満で到達判定
        if(direct.magnitude < 0.1f)
        {
            isRespawnTrace = false;
        }

        //実際のDampTime
        float realDampTime = (isRespawnTrace) ? respawnDampTime : dampTime;
        //ここまで

        Vector3 destination = cameraObj.transform.position + direct;                                    //目的地を計算
        cameraObj.transform.position = Vector3.SmoothDamp(cameraObj.transform.position, destination, ref velocity, realDampTime);   //スムーズダンプさせる
        //cameraObj.transform.position = Vector3.SmoothDamp(cameraObj.transform.position, destination, ref velocity, dampTime);   //スムーズダンプさせる
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
