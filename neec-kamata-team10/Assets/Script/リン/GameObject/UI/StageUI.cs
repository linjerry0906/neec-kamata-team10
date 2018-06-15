//------------------------------------------------------
// 作成日：2018.4.26
// 作成者：林 佳叡
// 内容：ステージUI表示
//------------------------------------------------------
using System;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField]
    private Text stageNumber;           //ステージ数
    [SerializeField]
    private Text currentTime;           //経過時間

    private StageManager stageManager;  //ステージマネージャー

    void Start ()
    {
        stageManager = GameManager.Instance.GetStageManager();          //ステージマネージャー取得
        stageNumber.text = stageManager.CurrentStage().ToString();      //ステージ設定
        currentTime.text = TimeString();                                //時間設定
	}
	
	void Update ()
    {
        currentTime.text = TimeString();                                //Timeの文字列更新
    }

    /// <summary>
    /// Timerの文字列
    /// </summary>
    /// <returns></returns>
    private string TimeString()
    {
        DateTime time = stageManager.PassTime();                                               //経過時間取得
        int milliSecond = time.Millisecond / 10;                                               //2桁にする
        return String.Format("{0:00}:{1:00}:{2:00}", time.Minute, time.Second, milliSecond);   //文字列化
    }
}
