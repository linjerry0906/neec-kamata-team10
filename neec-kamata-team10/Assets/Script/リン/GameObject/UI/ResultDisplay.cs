//------------------------------------------------------
// 作成日：2018.5.28
// 作成者：林 佳叡
// 内容：ゲーム結果を表示するクラス
//------------------------------------------------------
using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject stageObj;
    [SerializeField]
    private GameObject timeObj;
    [SerializeField]
    private GameObject scoreObj;

	void Start ()
    {
        StageManager stageManager = GameManager.Instance.GetStageManager();
        int stage = stageManager.CurrentStage();
        stageObj.GetComponent<Text>().text = stage.ToString();
        timeObj.GetComponent<Text>().text = TimeString(ref stageManager);
        int score = GameManager.Instance.GetScore();
        scoreObj.GetComponent<Text>().text = score.ToString();
    }

    /// <summary>
    /// Timerの文字列
    /// </summary>
    /// <returns></returns>
    private string TimeString(ref StageManager stageManager)
    {
        DateTime time = stageManager.PassTime();                                               //経過時間取得
        int milliSecond = time.Millisecond / 10;                                               //2桁にする
        return String.Format("{0:00}:{1:00}:{2:00}", time.Minute, time.Second, milliSecond);   //文字列化
    }
}
