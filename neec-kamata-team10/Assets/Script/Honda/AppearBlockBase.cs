using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearBlockBase : MonoBehaviour {

    protected bool IsReverse;
    protected bool isActive;           //現在のActive状態
    protected bool newActive;          //Update時の状態判定用bool

    protected float fadeT;             //fade時間
    protected float timeNow;           //経過時間

    protected GameObject appearObj; //スイッチで切り替えさせるObject

    //FadeColor関連
    protected Material objectMaterial;     //fade操作に使うObjectのMaterial
    protected Color colorNow, startColor;  //現在の色、Fade開始時の色
    protected Color maxColor, clearColor;  //α=max,0 Fade後の色


    protected enum fadeStatus //fade状態 していない,in,out
    {
        none,
        fadein,
        fadeout
    }

    protected fadeStatus fadeNow;

    // Use this for initialization
    protected void OriginStart()
    {
        isActive = IsReverse;        //初期状態の設定

        appearObj.SetActive(isActive);

        objectMaterial = appearObj.GetComponent<Renderer>().material;

        #region Fade関連の数値の設定
        startColor = objectMaterial.color;          //現在色取得

        maxColor = startColor;                      //a=maxの状態を保存
        clearColor = startColor; clearColor.a = 0f; //a=0も作成

        //Init時の色は反転設定=trueなら出現しているのでmax, falseなら消えているのでclear 
        colorNow = IsReverse ? maxColor : clearColor;

        fadeNow = fadeStatus.none;                //ステータスは変化なし
        #endregion
    }

    // Update is called once per frame
    protected void OriginUpdate()
    {

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
            appearObj.SetActive(true);
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

        else if (fadeNow == fadeStatus.fadein)   //フェードインなら
        {
            colorNow = Color.Lerp(startColor, maxColor, TimeRate());

            if (TimeRate() > 1f)
            {
                fadeNow = fadeStatus.none;
            }
        }

        else if (fadeNow == fadeStatus.fadeout)  //フェードアウトなら
        {
            colorNow = Color.Lerp(startColor, clearColor, TimeRate());

            if (TimeRate() >= 1f)              //fadeOut終了なら寝かせる
            {
                appearObj.SetActive(false);

                fadeNow = fadeStatus.none;
            }
        }

        objectMaterial.color = colorNow;
    }

    float TimeRate()
    {
        if (timeNow > fadeT) return 1f;
        return (timeNow / fadeT);
    }
}

