using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    private float limitTime = 0;
    private float currentTime = 0;
    
    /// <summary>
    /// 秒で指定
    /// </summary>
    /// <param name="limitTime"></param>
    public Timer(float limitTime)
    {
        this.limitTime = limitTime;
    }

    public void Initialize()
    {
        currentTime = 0;
    }

    /// <summary>
    /// タイマー更新
    /// </summary>
    public void TimeUpdate()
    {
        currentTime += Time.deltaTime;
    }

    /// <summary>
    /// 時間になったか
    /// </summary>
    /// <returns></returns>
    public bool IsTime()
    {
        if(limitTime <= currentTime)
        {
            currentTime = 0;
            return true;
        }

        return false;
    }

    public float Rate()
    {
        return (float)currentTime / limitTime;
    }
}
