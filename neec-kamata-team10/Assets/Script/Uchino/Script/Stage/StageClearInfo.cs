using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StageClearInfo : MonoBehaviour
{
    [SerializeField]
    private int stage = 1;

    [SerializeField]
    private readonly string filename = "StageClearData.sav";

    private void Dump()
    {
        Debug.Log(Stage);
    }

    /// <summary>
    /// ステージナンバーを返す
    /// </summary>
    /// <returns></returns>
    public int Stage
    {
        get { return stage; }
    }

    /// <summary>
    /// ステージをクリアしたナンバーを書き込む
    /// </summary>
    /// <param name="stage"></param>
    public void StageClear(int stage)
    {
        using (var sw = new StreamWriter(filename))
        {
            sw.WriteLine(stage);
        }
    }

    /// <summary>
    /// ステージクリア状態を読み込む、クリアされていなければstageは0
    /// </summary>
    public void Load()
    {
        try
        {
            using (var sr = new StreamReader(filename))
            {
                string text = sr.ReadLine();
                stage = int.Parse(text);
            }
        }
        catch
        {
            stage = 0;
        }

    }

    


}


