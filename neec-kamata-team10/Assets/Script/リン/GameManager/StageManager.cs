﻿//------------------------------------------------------
// 作成日：2018.4.26
// 作成者：林 佳叡
// 内容：ステージを管理するマネージャー
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StageManager
{
    private int currentStage;                   //現在のステージ
    private bool isStage;                       //ステージ中か
    private DateTime passTime;                  //経過時間

    public StageManager()
    {
        currentStage = 0;
        isStage = false;
        passTime = DateTime.MinValue;
    }

    /// <summary>
    /// ステージ情報初期化
    /// </summary>
    /// <param name="nextStage">次のステージ</param>
    public void Initialize(int nextStage)
    {
        currentStage = nextStage;               //ステージ指定
        passTime = DateTime.MinValue;           //Reset
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Update()
    {
        if (!isStage)                           //Stage以外は処理しない
            return;

        passTime = passTime.AddSeconds(Time.deltaTime);    //経過時間を加算
    }

    /// <summary>
    /// 現在のステージ
    /// </summary>
    /// <returns></returns>
    public int CurrentStage()
    {
        return currentStage;
    }

    /// <summary>
    /// 時間計算開始
    /// </summary>
    public void StartStage()
    {
        isStage = true;
        passTime = DateTime.MinValue;           //Reset
    }

    /// <summary>
    /// 時間計算終了
    /// </summary>
    public void EndStage()
    {
        isStage = false;
    }

    /// <summary>
    /// 経過時間を取得
    /// </summary>
    /// <returns></returns>
    public DateTime PassTime()
    {
        return passTime;
    }
}
