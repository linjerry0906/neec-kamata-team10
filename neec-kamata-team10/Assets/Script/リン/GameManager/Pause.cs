//------------------------------------------------------
// 作成日：2018.6.08
// 作成者：林 佳叡
// 内容：PauseScene
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private GameManager gameManager;
    private StageManager stageManager;
    private ICharacterController controller;

    void Start()
    {
        gameManager = GameManager.Instance;
        //時間計算停止
        stageManager = gameManager.GetStageManager();
        stageManager.EndStage();
        //コントローラーをロック
        controller = gameManager.GetController();
        controller.SetFadeFlag(true);
        //時間停止
        Time.timeScale = 0;
    }
	
	void Update ()
    {
        if (!controller.SwitchToTheLeft())
            return;

        //時間計算開始
        stageManager.Resume();
        Time.timeScale = 1;
        //コントローラー操作可能
        controller.SetFadeFlag(false);
        //シーン切り替え
        gameManager.Return();
    }
}
