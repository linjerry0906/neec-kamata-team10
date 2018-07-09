///<summary>
/// チュートリアルメッセージ表示用Objectのスクリプト
/// 作成者 本田
/// 作成日 2018/6/1
/// 最終更新 2018/6/1
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialObject : MonoBehaviour {

    [SerializeField]
    private Text tutorialText;    //テキストUI
    [SerializeField]
    private string message;       //メッセージ内容
    [SerializeField]
    private float fadeTime = 0.5f;  //フェードタイム

    private Color color;          //フォントのColor値

    private enum TextNow
    {
        none,
        fadeIn,
        appear,
        fadeOut
    }

    private TextNow textNow;      //テキストの表示ステータス

	// Use this for initialization
	void Start () {
        textNow = TextNow.none;

        color = tutorialText.color;
        color.a = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (textNow == TextNow.none || textNow == TextNow.appear) return; //変化がない場合はreturn

        //変化がある場合
        //else
		if (textNow == TextNow.fadeIn)
        {
            color.a += Time.deltaTime / fadeTime;

            //In終わったら
            if (color.a >= 1f)
            {
                color.a = 1f;
                textNow = TextNow.appear;
            }
        }

        else //if (textNow == TextNow.fadeOut)
        {
            color.a -= Time.deltaTime / fadeTime;

            //Out終わったら このタイミングでText消す
            if (color.a <= 0f)
            {
                color.a = 0f;
                textNow = TextNow.none;
                tutorialText.text = "";              //テキストUIの中身を消去
            }
        }

        tutorialText.color = color;
	}

    void OnTriggerEnter(Collider other) //プレイヤーと接触したらTextを変更 Stayだと処理が重そう
    {
        if (!other.tag.Equals("Player")) return; //プレイヤー以外に用はない

        else                            //プレーヤーと接触したら
        {
            tutorialText.text = message;         //共通テキストUIの中身を設定されたmessageに変更
            textNow = TextNow.fadeIn;            //fadeInに変更
        }
    }

    void OnTriggerExit(Collider other)  //プレイヤーとの接触が終わったときにTextを消去
    {
        if (!other.tag.Equals("Player")) return; //プレイヤー以外に用はない

        else                            //プレーヤーが離れた場合
        {
            textNow = TextNow.fadeOut;
        }
    }
}
