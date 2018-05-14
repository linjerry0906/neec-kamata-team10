﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingEnemy : MoveEnemy {

    public GameObject throwingObject;

    private Timer throwtimer;
    [SerializeField]
    private float throwInterval = 2f;


    // Use this for initialization
    void Start () {
        DirectionInit();
        throwtimer = new Timer(throwInterval);
    }

    // Update is called once per frame
    void Update () {
        SetGroundEdge();                    //地面端の設定
        HorizontalMove();                   //水平移動

        InstanceThrowObj();
    }

    /// <summary>
    /// 投げる
    /// </summary>
    void InstanceThrowObj()
    {
        throwtimer.TimeUpdate();            //投げる間隔

        if (!throwtimer.IsTime()) return;   //投げる時間になってなかったら生成しない

        //投げる物の位置
        Vector3 throwingObjectPos
            = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);

        //自分の向いている方向をセットして　インスタンス
        Instantiate(throwingObject, throwingObjectPos, Quaternion.identity)
            .GetComponent<Throw>().SetDircetion(Direction);
    }
}
