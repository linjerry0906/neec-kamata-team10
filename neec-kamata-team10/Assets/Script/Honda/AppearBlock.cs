using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作成日   2018/6/9
/// 最終更新 2018/6/11
/// 作成者   本田尚大
/// 
/// 使い方
///     Objectの Active = false にするとコードまで無効になるので
///     Emptyにこのコードを持たせてEmptyのChildとして対象のObjectを持たせてください。
///     UnityのGameObject.Activeを切り替えているのでObjectならなんでも操作できるはず。
///     
/// フェード周りの仕様
///     ・完全にFadeOutするまでは(Unityの仕様的にも)Activeにしておく。
///     
/// </summary>

public class AppearBlock : MonoBehaviour
{
    public bool IsReverseAppear = false; //trueだとスイッチOnで消滅、スイッチOffで出現
    bool isActive;                       //現在のActive状態

    [SerializeField]
    public SwitchObject switchObj; //スイッチ本体(のコード)
    public GameObject appearObject; //スイッチで切り替えさせるObject

    //FadeTime関連
    [SerializeField]
    private float fadeTime = 0.2f;  //fade時間
    private float timeNow;          //経過時間

    //FadeColor関連
    private Material objectMaterial;     //fade操作に使うObjectのMaterial
    private Color colorNow, startColor;  //現在の色、Fade開始時の色
    private Color maxColor, clearColor;  //α=max,0 Fade後の色


    enum fadeStatus //fade状態 していない,in,out
    {
        none,
        fadein,
        fadeout
    }

    fadeStatus fadeNow;

	// Use this for initialization
	void Start () {
        isActive = IsReverseAppear;        //初期状態の設定
        appearObject.SetActive(isActive);

        objectMaterial = appearObject.GetComponent<Renderer>().material;

        #region Fade関連の数値の設定
        startColor = objectMaterial.color;          //現在色取得

        maxColor = startColor;                      //a=maxの状態を保存
        clearColor = startColor; clearColor.a = 0f; //a=0も作成

        //Init時の色は反転設定=trueなら出現しているのでmax, falseなら消えているのでclear 
        colorNow = IsReverseAppear ? maxColor : clearColor;

        fadeNow = fadeStatus.none;                //ステータスは変化なし
        #endregion
    }
	
	// Update is called once per frame
	void Update () {

        if (fadeNow != fadeStatus.none)   //タイマーの時間分岐
        {
            timeNow += Time.deltaTime;
            SetAlpha();
        }


        ///<summary>
        /// Activeの条件は
        /// 
        ///                 |  switch-On?
        ///      IsReverse? | false | true
        /// ----------------+-------+-------
        ///  normal = false | false | true
        /// reverse =  true | true  | false
        /// 
        /// なのでXORで設定できる
        ///</summary>

        //現在の状態と新しい状態を比較 違う=XORがtrue
        bool newActive = switchObj.IsTurnOn ^ IsReverseAppear;

        if (isActive ^ newActive)
        {
            Judge();
            timeNow = 0f;
            startColor = colorNow;
            SetAlpha();        //初回Alpha値設定
        }
	}

    //切り替え時の状態ジャッジ&処理Class
    void Judge()
    {
        if (!isActive) //元々寝ていた場合->起こす
        {
            isActive = true; 
            appearObject.SetActive(true);
            fadeNow = fadeStatus.fadein;
        }

        else //起きていた場合
        {
            isActive = false; //fade終了まで実際のActiveは消さない
            fadeNow = fadeStatus.fadeout;
        }

    }

    void SetAlpha()
    {
        if (fadeNow == fadeStatus.none) return; //fadeしていないのになんで来たんだ

        else if(fadeNow == fadeStatus.fadein)   //フェードインなら
        {
            colorNow = Color.Lerp(startColor, maxColor, TimeRate());

            if(TimeRate() > 1f)
            {
                fadeNow = fadeStatus.none;
            }
        }

        else if(fadeNow == fadeStatus.fadeout)  //フェードアウトなら
        {
            colorNow = Color.Lerp(startColor, clearColor, TimeRate());

            if(TimeRate() >= 1f)              //fadeOut終了なら寝かせる
            {
                appearObject.SetActive(false);

                fadeNow = fadeStatus.none;
            }
        }

        objectMaterial.color = colorNow;
    }

    float TimeRate()
    {
        if (timeNow > fadeTime) return 1f;
        return (timeNow / fadeTime);
    }
}
