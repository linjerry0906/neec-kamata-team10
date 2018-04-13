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

    public TraceCharacter(GameObject camera, GameObject target, Vector3 relativePos, float radius)
    {
        this.camera = camera;
        this.target = target;
        this.relativePos = relativePos;
        this.radius = radius;
    }

    /// <summary>
    /// 追尾
    /// </summary>
    public void Trace()
    {
        Vector3 dest = target.transform.position + relativePos;                 //目的地
        Vector3 dir = dest - camera.transform.position;                         //方向ベクトル

        float length = dir.magnitude;                                           //距離
        if (length < radius)                                                    //範囲内は移動しない
            return;

        dir /= length;                                                          //正規化
        camera.transform.position += dir * (length - radius) * Time.deltaTime;  //移動させる
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
