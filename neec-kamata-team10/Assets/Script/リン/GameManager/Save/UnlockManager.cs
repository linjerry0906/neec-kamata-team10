//------------------------------------------------------
// 作成日：2018.6.25
// 作成者：林 佳叡
// 内容：ステージ解除するかを管理するクラス
//------------------------------------------------------
using System;
using UnityEngine;

public class UnlockManager
{
    private bool[] stages;
    private DateTime[] clearTimes;

    public UnlockManager()
    {
        stages = new bool[(int)(EScene.StageNull - 1)];
        stages[0] = true;

        clearTimes = new DateTime[(int)(EScene.StageNull - 1)];
    }

    /// <summary>
    /// 指定のステージが解放されたか
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    public bool IsUnlocked(EScene stage)
    {
        return stages[ClampIndex(stage)];
    }

    /// <summary>
    /// クリアタイム
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    public DateTime ClearTime(EScene stage)
    {
        return clearTimes[ClampIndex(stage)];
    }

    /// <summary>
    /// クリア
    /// </summary>
    /// <param name="nextStage"></param>
    public void Clear(EScene currentStage, EScene nextStage, DateTime time)
    {
        int next = ClampIndex(nextStage);
        int current = ClampIndex(currentStage);

        UpdateTimeScore(current, time);     //Time更新
        stages[next] = true;
    }

    /// <summary>
    /// TimeScore更新処理
    /// </summary>
    /// <param name="index"></param>
    /// <param name="time"></param>
    private void UpdateTimeScore(int index, DateTime time)
    {
        if (clearTimes[index] == DateTime.MinValue)         //初回クリアの場合
        {
            clearTimes[index] = time;       //Time記録
            return;
        }

        TimeSpan span = clearTimes[index].Subtract(time);
        if (span.Ticks > 0)                 //より短い時間の場合
            clearTimes[index] = time;
    }

    /// <summary>
    /// Enumから添え字を変換
    /// </summary>
    /// <param name="stage">ステージEnum</param>
    /// <returns></returns>
    private int ClampIndex(EScene stage)
    {
        int maxStage = (int)(EScene.StageNull - 2);         //最大ステージ（添え字-1、Enum-1）
        int index = (int)(stage - 1);                       //指定のステージを添え字に変換
        index = Mathf.Min(maxStage, Mathf.Max(index, 0));   //バグ対策

        return index;
    }

    /// <summary>
    /// セーブ用のString
    /// </summary>
    /// <returns></returns>
    public string SaveString(int stage)
    {
        int index = ClampIndex((EScene)stage);              //添え字をクランプ
        int isClear = stages[index] ? 1 : 0;                //1 clear 0 unclear
        DateTime time = clearTimes[index];
        string saveString = stage.ToString() + "," + isClear.ToString() + "," + time.Minute + "," + time.Second + "," + time.Millisecond;
        return saveString;
    }

    /// <summary>
    /// Load処理
    /// </summary>
    /// <param name="saveData"></param>
    public void Load(string saveData)
    {
        string[] saveStrings = saveData.Split(',');
        int stage = int.Parse(saveStrings[0]);              //Stage
        bool isClear = int.Parse(saveStrings[1]) == 1 ? true : false;                       //クリアしたか
        int minute = int.Parse(saveStrings[2]);             //分
        int second = int.Parse(saveStrings[3]);             //秒
        int milliSec = int.Parse(saveStrings[4]);           //ミリ秒
        DateTime clearTime = new DateTime();                //クリアタイム
        clearTime = clearTime.AddMinutes(minute);           //分設定
        clearTime = clearTime.AddSeconds(second);           //秒設定
        clearTime = clearTime.AddMilliseconds(milliSec);    //ミリ秒設定

        int index = ClampIndex((EScene)stage);              //添え字をクランプ
        stages[index] = isClear;
        clearTimes[index] = clearTime;
    }
}
