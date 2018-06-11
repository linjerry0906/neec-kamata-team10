//------------------------------------------------------
// 作成日：2018.6.11
// 作成者：林 佳叡
// 内容：PauseシーンのパネルFade処理
//------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class PausePanelFade : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseManager;
    [SerializeField]
    private float fadeSpeed = 0.5f;     //fadeするスピード
    [SerializeField]
    private float maxAlpha = 0.25f;     //Alpha最大値

    private float fadeAlpha = 0;        //現在のAlpha
    private FadeState fadeState;        //Fade状態

    private Image panelImage;

    private void Start()
    {
        panelImage = GetComponent<Image>();
        panelImage.color = new Color(1, 1, 1, fadeAlpha);       //Alpha設定
        fadeState = FadeState.FadeIn;                           //初期状態をFadeIn
    }

    private void Update()
    {
        switch (fadeState)
        {
            case FadeState.FadeIn:
                UpdateFadeIn();
                break;
            case FadeState.None:
                return;
            case FadeState.FadeOut:
                UpdateFadeOut();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// FadeIn処理
    /// </summary>
    private void UpdateFadeIn()
    {
        fadeAlpha += Time.unscaledDeltaTime * fadeSpeed;    //Fadeする

        if (fadeAlpha >= maxAlpha)                          //超えたら
        {
            fadeAlpha = maxAlpha;
            fadeState = FadeState.None;
        }

        panelImage.color = new Color(1, 1, 1, fadeAlpha);       //Alpha設定
    }

    /// <summary>
    /// FadeOut処理
    /// </summary>
    private void UpdateFadeOut()
    {
        fadeAlpha -= Time.unscaledDeltaTime * fadeSpeed;    //Fadeする

        if (fadeAlpha <= 0)                                 //超えたら
        {
            fadeAlpha = 0;
            fadeState = FadeState.End;
            pauseManager.GetComponent<Pause>().Resume();    //ゲーム再開
        }

        panelImage.color = new Color(1, 1, 1, fadeAlpha);       //Alpha設定
    }

    /// <summary>
    /// FadeStateを設定
    /// </summary>
    /// <param name="state"></param>
    public void SetFadeState(FadeState state)
    {
        fadeState = state;
    }

    /// <summary>
    /// Fade終了したか
    /// </summary>
    /// <returns></returns>
    public bool IsEnd()
    {
        return fadeState == FadeState.End;
    }

    /// <summary>
    /// FadeRate
    /// </summary>
    /// <returns></returns>
    public float FadeRate()
    {
        return fadeAlpha / maxAlpha;
    }
}
