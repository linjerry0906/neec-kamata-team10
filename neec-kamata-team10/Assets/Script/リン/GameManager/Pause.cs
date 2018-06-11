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
    private GameObject pausePanel;
    [SerializeField]
    private GameObject[] uiImage;
    [SerializeField]
    private float bgmMaxVolume = 0.3f;
    private float previousVolume;

    private GameManager gameManager;                //GameManager
    private StageManager stageManager;              //StageManager
    private SoundManager soundManager;              //SoundManager
    private ICharacterController controller;        //コントローラー

    private PauseSelectEnum select = PauseSelectEnum.Retry;

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

        uiImage[0].GetComponent<PauseSelectAnime>().SetVisible(true);
    }

    void Update()
    {
        if (controller.Pause())
        {
            pausePanel.GetComponent<PausePanelFade>().SetFadeState(FadeState.FadeOut);
        }

        Select();
    }

    /// <summary>
    /// 選択
    /// </summary>
    private void Select()
    {
        int selectInt = (int)select;
        if (controller.MoveSelectionDown())         //下
            selectInt++;
        if (controller.MoveSelectionUp())           //上
            selectInt--;

        selectInt = Mathf.Abs(selectInt);
        selectInt %= (int)PauseSelectEnum.Null;

        uiImage[(int)select].GetComponent<PauseSelectAnime>().SetVisible(false);
        uiImage[selectInt].GetComponent<PauseSelectAnime>().SetVisible(true);
        select = (PauseSelectEnum)selectInt;
    }

    /// <summary>
    /// ゲーム再開
    /// </summary>
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
