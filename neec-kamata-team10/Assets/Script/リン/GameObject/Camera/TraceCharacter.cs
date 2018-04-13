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
    private GameObject camera;
    private GameObject target;
    private Vector3 relativePos;
    private float range;

    public TraceCharacter(GameObject camera, GameObject target, Vector3 relativePos, float radius)
    {
        this.camera = camera;
        this.target = target;
        this.relativePos = relativePos;
    }

    /// <summary>
    /// 追尾
    /// </summary>
    public void Trace()
    {
        Vector3 dest = target.transform.position + relativePos;
        Vector3 dir = dest - camera.transform.position;

        float length = dir.magnitude;
        if (length < 3)
            return;

        dir /= length;
        camera.transform.position += dir * (length - 3) * 0.01f;
    }
}
