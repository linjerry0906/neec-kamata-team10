//------------------------------------------------------
// 作成日：2018.6.11
// 作成者：林 佳叡
// 内容：PauseシーンのTextFade処理
//------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class PauseTextColor : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;      //Pauseのパネル

    private Text text;                  //テキストコンポーネント
    private PausePanelFade fadePanel;   //Fadeを管理するパネル

    void Start ()
    {
        fadePanel = pausePanel.GetComponent<PausePanelFade>();
        text = GetComponent<Text>();
        SetColor();
	}
	
	void Update ()
    {
        SetColor();
    }

    /// <summary>
    /// 色設定のメソッド
    /// </summary>
    private void SetColor()
    {
        Color color = text.color;           //色取得
        color.a = fadePanel.FadeRate();     //Alpha設定
        text.color = color;                 //色設定
    }
}
