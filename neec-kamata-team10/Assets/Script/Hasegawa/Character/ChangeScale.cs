using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale
{
    private Mirror mirror;                 //対象の鏡
    private bool isChange = false;         //変形しているか
    private float changeTime;          //鏡の範囲外に行ってから元の大きさに戻るまでの時間
    private float timer = 0;               //changeTimeをカウントするためのタイマー

    public ChangeScale(Mirror mirror,float changeTime)
    {
        this.mirror = mirror;
        this.changeTime = changeTime;
    }

    //mirrorの設定
    public void SetMirror(Mirror mirror)
    {
        this.mirror = mirror;
    }

    //changeTimeの設定
    public void SetChangeTime(float changeTime)
    {
        this.changeTime = changeTime;
    }

    //オブジェクトの変形
    public Vector3 Scale(Vector2 position)
    {
        if (CheckReflect(position))
        {
            isChange = true;
            timer = changeTime;
        }
        if (isChange)
        {
            TimerUpdate();
            return mirror.ReflectSize();
        }
        else return new Vector3(1, 1, 1);
    }

    //鏡の範囲内にオブジェクトがあるか
    bool CheckReflect(Vector2 position)
    {
        if (mirror.GetSide().xMin > position.x) return false;
        if (mirror.GetSide().xMax < position.x) return false;
        if (mirror.GetSide().yMax < position.y) return false;
        if (mirror.GetSide().yMin > position.y) return false;
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