﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale
{
    private bool isChange = false;         //変形しているか
    private float changeTime;              //鏡の範囲外に行ってから元の大きさに戻るまでの時間
    private float timer = 0;               //changeTimeをカウントするためのタイマー
    private Vector3 mirrorSize;

    public ChangeScale(Vector3 mirrorSize,float changeTime)
    {
        this.mirrorSize = mirrorSize;
        this.changeTime = changeTime;
    }

    //mirrorSizeの設定
    public void SetMirrorSize(Vector3 mirrorSize)
    {
        this.mirrorSize = mirrorSize;
    }

    //changeTimeの設定
    public void SetChangeTime(float changeTime)
    {
        this.changeTime = changeTime;
    }

    //オブジェクトの変形
    public Vector3 Scale(SizeEnum size)
    {
        if (CheckReflect(size))
        {
            isChange = true;
            timer = changeTime;
        }
        if (isChange)
        {
            TimerUpdate();
            return mirrorSize;
        }
        else return new Vector3(1, 1, 1);
    }

    //鏡の範囲内にオブジェクトがあるか
    bool CheckReflect(SizeEnum size)
    {
        if (size == SizeEnum.Normal) return false;
        return true;
    }

    //タイマーの更新
    void TimerUpdate()
    {
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            timer = changeTime;
            isChange = false;
        }
    }
}