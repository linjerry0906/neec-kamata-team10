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

    public float fadeTime = 0.2f;  //fade時間
    Material objectMaterial;       //fade操作に使うObjectのMaterial
    int timer, limitTime;          //fade用のtimer(笑)

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
        limitTime = (int)(fadeTime * 60f); //60fps前提の簡易fade

        objectMaterial = appearObject.GetComponent<Renderer>().material;

        timer = 0;

        fadeNow = fadeStatus.none;
	}
	
	// Update is called once per frame
	void Update () {

        if (timer < limitTime)   //タイマーの時間分岐
        {
            timer++;
            SetAlpha();
        }

        else
        {
            fadeNow = fadeStatus.none;
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
            SetTimer();
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
        Color objColor = objectMaterial.color;
        if (fadeNow == fadeStatus.none) return; //fadeしていないのになんで来たんだ

        else if(fadeNow == fadeStatus.fadein)   //フェードインなら
        {
            objColor.a = GetTime();
        }

        else if(fadeNow == fadeStatus.fadeout)  //フェードアウトなら
        {
            objColor.a = 1.0f - GetTime();

            if(timer >= limitTime)              //fadeOut終了なら寝かせる
            {
                appearObject.SetActive(false);
            }
        }

        objectMaterial.color = objColor;
    }

    void SetTimer()
    {
        if (timer >= limitTime) //前回のフェードが終了していれば
        {
            timer = 0;
        }

        else                           //スイッチを高速で連打してフェードが終わってなければ
        {
            timer = limitTime - timer; //経過時間を取得して途中から
        }
    }

    float GetTime()
    {
        return (float)timer / (float)limitTime;
    }
}
