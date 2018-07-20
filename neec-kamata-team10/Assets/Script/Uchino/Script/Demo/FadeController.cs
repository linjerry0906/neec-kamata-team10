using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// フェードモード
/// </summary>
enum EfadeMode
{
    FADE_IN_ONLY,
    FADE_OUT_ONLY,
    NOMAL
};

/// <summary>
/// 遷移先シーン
/// </summary>
enum EFadeScene
{
    Title,
    Demo
};

public class FadeController : MonoBehaviour {

    Image fadeImage;                //fade用のパネル
    float red, blue, green, alpha;  //色、不透明度管理
    bool isFadeOut = false;         //フェードアウトしたか
    bool isFadeIn = false;          //フェードインしたか

    [SerializeField]
    float fadeInSpeed = 0.2f;       //フェードインするスピード
    [SerializeField]
    float fadeOutSpeed = 1f;        //フェードアウトするスピード
    [SerializeField]
    EfadeMode fadeMode;             //フェードモード
    [SerializeField]
    EFadeScene fadeScene;           //フェード先シーン

	// Use this for initialization
	void Start () {
        fadeImage = GetComponent<Image>();
        
        //カラーと不透明度の取得
        red   = fadeImage.color.r;
        green = fadeImage.color.g;
        blue  = fadeImage.color.b;
        alpha = InitializeAlpha();
    }

    /// <summary>
    /// アルファ値の決定
    /// </summary>
    /// <returns></returns>
    int InitializeAlpha()
    {
        return (fadeMode == EfadeMode.FADE_OUT_ONLY) ? 0 : 1;
    }

    // Update is called once per frame
    void Update () {

        //NOMALならフェードインフェードアウト両方行う
        if(fadeMode == EfadeMode.NOMAL)
        {
            FadeInOnly();
            FadeOutOnly();
            return;
        }
        //フェードアウトだけ
        if(fadeMode == EfadeMode.FADE_OUT_ONLY)
        {
            FadeOutOnly();
            return;
        }

        //フェードインだけ
        FadeInOnly();
    }


    //フェードアウトがスタートされたら
    void FadeOutOnly()
    {
        if (isFadeOut) FadeOut();
    }

    //初めからフェードインを行う
    void FadeInOnly()
    {
        fadeImage.enabled = true;
        if (!isFadeIn) FadeIn();
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    void FadeOut()
    {
        //徐々に不透明度を上げていく
        alpha += fadeOutSpeed * Time.deltaTime;
        SetAlpha();

        //フェード仕切ったらシーンを遷移する
        if (alpha >= 1)
        {
            LoadScene();
        }
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    void FadeIn()
    {
        //徐々に透明度を上げていく
        alpha -= fadeInSpeed * Time.deltaTime;
        SetAlpha();
        
        //透明になり切ったら
        if (alpha <= 0)
        {
            isFadeIn = true;
            fadeImage.enabled = false;
        }
    }

    /// <summary>
    /// 透明度を反映
    /// </summary>
    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpha);
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    void LoadScene()
    {
        SceneManager.LoadScene(fadeScene.ToString());
    }
    
    /// <summary>
    /// フェードアウトを始める
    /// </summary>
    public void FadeOutStart()
    {
        isFadeOut = true;
        fadeImage.enabled = true;
    }
}
