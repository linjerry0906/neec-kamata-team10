//------------------------------------------------------
// 作成日：2018.5.28
// 作成者：林 佳叡
// 内容：クリア時に選択できる項目
//------------------------------------------------------
using UnityEngine;

public class ClearSelect : MonoBehaviour
{
    private enum ESelection
    {
        StageSelect = 0,
        TryAgain,
        Lock,
    }

    [SerializeField]
    private GameObject selectButton;
    [SerializeField]
    private GameObject tryButton;

    private GameObject[] buttons;                   //ボタン
    private ICharacterController controller;        //コントローラー
    private ESelection currentSelect;               //現在選択中のボタン

    private void Start()
    {
        controller = GameManager.Instance.GetController();
        currentSelect = ESelection.StageSelect;

        InitButton();                               //ボタンの初期化
    }

    /// <summary>
    /// ボタン配列の初期化
    /// </summary>
    private void InitButton()
    {
        buttons = new GameObject[(int)ESelection.Lock];
        buttons[(int)ESelection.StageSelect] = selectButton;
        buttons[(int)ESelection.TryAgain] = tryButton;
    }

    private void Update()
    {
        if (currentSelect == ESelection.Lock)       //ロックされた場合は操作できない
            return;

        Choose();                                   //選択処理
        AnimateButton();                            //ボタンのアニメーシ
        Select();                                   //選択された時の処理
    }

    /// <summary>
    /// 選択処理
    /// </summary>
    private void Choose()
    {
        int index = (int)currentSelect;
        if (controller.MoveSelectionDown())
            index++;
        if (controller.MoveSelectionUp())
            index--;

        index = Mathf.Abs(index) % (int)ESelection.Lock;        //クランプ
        currentSelect = (ESelection)index;                      //設定しなおし
    }

    /// <summary>
    /// ボタンが選択されたときのアニメーシ
    /// </summary>
    private void AnimateButton()
    {
        for (int i = 0; i < buttons.Length; ++i)
        {
            bool visible = (int)currentSelect == i ? true : false;                      //選択されたボタンのみ見える
            buttons[i].GetComponentInChildren<PauseSelectAnime>().SetVisible(visible);
        }
    }

    /// <summary>
    /// セレクトボタンが押された処理
    /// </summary>
    private void Select()
    {
        if (!controller.Jump())
            return;

        if (currentSelect == ESelection.TryAgain)
        {
            GameManager.Instance.TrySameStage(true);
            currentSelect = ESelection.Lock;                    //ロックする
            return;
        }
        GameManager.Instance.ChangeScene(EScene.StageSelect);
        currentSelect = ESelection.Lock;                        //ロックする
    }
}
