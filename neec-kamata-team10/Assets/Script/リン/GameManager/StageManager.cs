//------------------------------------------------------
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

    private bool isClear;

    private Vector3 startPos;                   //Player初期位置
    private Vector3 cameraPos;                  //カメラ初期位置

    public StageManager()
    {
        currentStage = 0;
        isStage = false;
        isClear = false;
        passTime = DateTime.MinValue;
    }

    /// <summary>
    /// ステージ情報初期化
    /// </summary>
    /// <param name="nextStage">次のステージ</param>
    public void Initialize(int nextStage, bool resetTime)
    {
        currentStage = nextStage;               //ステージ指定
        isClear = false;
        if (resetTime)
        {
            passTime = DateTime.MinValue;       //Reset
            startPos = Vector3.zero;
            cameraPos = Vector3.zero;
        }
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

    /// <summary>
    /// クリアかどうかを知らせる
    /// </summary>
    /// <param name="isClear">クリアしたか</param>
    public void SetClear(bool isClear)
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.GetController().SetFadeFlag(true);                      //操作禁止
        Time.timeScale = 0;

        this.isClear = isClear;
        gameManager.GetComponent<ResultManager>().GameOver(isClear);
    }

    /// <summary>
    /// Playerのスタート場所
    /// </summary>
    /// <returns></returns>
    public Vector3 StartPos()
    {
        return startPos;
    }

    /// <summary>
    /// Playerのスタート場所を設定
    /// </summary>
    /// <param name="pos">Respawn位置</param>
    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
    }

    /// <summary>
    /// カメラの初期位置
    /// </summary>
    /// <returns></returns>
    public Vector3 CameraPos()
    {
        return cameraPos;
    }

    /// <summary>
    /// Cameraスタート位置設定
    /// </summary>
    public void SetCameraPos()
    {
        cameraPos = Camera.main.transform.position;
    }
}
