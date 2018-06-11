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
    [SerializeField]
    private GameObject PausePanel;
    [SerializeField]
    private float bgmMaxVolume = 0.3f;
    private float previousVolume;

    private GameManager gameManager;                //GameManager
    private StageManager stageManager;              //StageManager
    private SoundManager soundManager;              //SoundManager
    private ICharacterController controller;        //コントローラー

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
        //背景音量設定
        soundManager = gameManager.GetSoundManager();
        previousVolume = soundManager.MaxVolume();
        soundManager.SetMaxVolume(bgmMaxVolume);
    }
	
	void Update ()
    {
        if (!controller.SwitchToTheLeft())
            return;

        PausePanel.GetComponent<PausePanelFade>().SetFadeState(FadeState.FadeOut);
    }

    public void Resume()
    {
        //時間計算開始
        stageManager.StartStage();
        Time.timeScale = 1;
        //コントローラー操作可能
        controller.SetFadeFlag(false);
        //シーン切り替え
        gameManager.Resume();
        //背景音量設定
        gameManager.GetSoundManager().SetMaxVolume(previousVolume);
    }
}
