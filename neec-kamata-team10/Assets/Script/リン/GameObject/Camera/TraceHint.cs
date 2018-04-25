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

    public TraceHint(GameObject target, GameObject cameraObj, float dampTime)
    {
        this.target = target;
        this.cameraObj = cameraObj;
        camera = cameraObj.GetComponent<Camera>();
        this.dampTime = dampTime;
    }

    /// <summary>
    /// 追尾
    /// </summary>
    public void Trace()
    {
        if (!target)                            //注目先がない場合はTraceしない
            return;

        Vector3 targetPos = target.transform.position;                                                  //注目先の位置
        Vector3 point = camera.WorldToViewportPoint(targetPos);                                         //画面上の位置に変換
        Vector3 direct = targetPos - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));     //同じ深度で方向を計算する
        Vector3 destination = cameraObj.transform.position + direct;                                    //目的地を計算
        cameraObj.transform.position = Vector3.SmoothDamp(cameraObj.transform.position, destination, ref velocity, dampTime);   //スムーズダンプさせる
    }

    /// <summary>
    /// ターゲットを設定
    /// </summary>
    /// <param name="target">ターゲット</param>
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
