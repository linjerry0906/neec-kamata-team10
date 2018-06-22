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
    private SystemSE systemSe;                      //SystemSe
    private ICharacterController controller;        //コントローラー

    private PausePanelFade fadeManager;             //Fadeのマネージャー

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
        systemSe = gameManager.GetSystemSE();
        previousVolume = soundManager.MaxVolume();
        soundManager.SetMaxVolume(bgmMaxVolume);

        uiImage[0].GetComponent<PauseSelectAnime>().SetVisible(true);
        fadeManager = pausePanel.GetComponent<PausePanelFade>();
    }

    void Update()
    {
        if (fadeManager.CurrentState() != FadeState.None)
            return;

        PauseButton();
        Select();
        Trigger();
    }

    /// <summary>
    /// Pauseボタン
    /// </summary>
    private void PauseButton()
    {
        if (!controller.Pause())
            return;
        systemSe.PlaySystemSE(SystemSoundEnum.se_cancel);
        fadeManager.SetFadeState(FadeState.FadeOut);
    }

    /// <summary>
    /// 選択
    /// </summary>
    private void Select()
    {
        int selectInt = (int)select;
        if (controller.MoveSelectionDown())         //下
        {
            selectInt++;
            systemSe.PlaySystemSE(SystemSoundEnum.se_select);
        }
        if (controller.MoveSelectionUp())           //上
        {
            selectInt--;
            systemSe.PlaySystemSE(SystemSoundEnum.se_select);
        }

        selectInt = Mathf.Abs(selectInt);
        selectInt %= (int)PauseSelectEnum.Null;     //添え字計算

        uiImage[(int)select].GetComponent<PauseSelectAnime>().SetVisible(false);
        uiImage[selectInt].GetComponent<PauseSelectAnime>().SetVisible(true);
        select = (PauseSelectEnum)selectInt;
    }

    /// <summary>
    /// トリガーボタン
    /// </summary>
    private void Trigger()
    {
        if (!controller.Jump())
            return;

        if (select == PauseSelectEnum.Retry)
            Retry();

        if (select == PauseSelectEnum.StageSelect)
            StageSelect();
    }

    /// <summary>
    /// リトライ
    /// </summary>
    private void Retry()
    {
        gameManager.PauseRetry();
    }

    /// <summary>
    /// ステージセレクト
    /// </summary>
    private void StageSelect()
    {
        gameManager.ChangeScene(EScene.StageSelect);
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
