//------------------------------------------------------
// 作成日：2018.6.25
// 作成者：林 佳叡
// 内容：ステージ解除するかを管理するクラス
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager
{
    private bool[] stages;

    public UnlockManager()
    {
        stages = new bool[(int)(EScene.StageNull - 1)];
        stages[0] = true;
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
    /// クリア
    /// </summary>
    /// <param name="stage"></param>
    public void Clear(EScene stage)
    {
        stages[ClampIndex(stage)] = true;
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
}
