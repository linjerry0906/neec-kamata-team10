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
    public Text tutorialText; //テキストUI

    public string message; //メッセージ内容

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) //プレイヤーと接触したらTextを変更 Stayだと処理が重そう
    {
        if (!other.tag.Equals("Player")) return; //プレイヤー以外に用はない

        else                            //プレーヤーと接触したら
        {
            tutorialText.text = message;         //テキストUIの中身を設定されたmessageに変更
        }
    }

    void OnTriggerExit(Collider other)  //プレイヤーとの接触が終わったときにTextを消去
    {
        if (!other.tag.Equals("Player")) return; //プレイヤー以外に用はない

        else                            //プレーヤーが離れた場合
        {
            tutorialText.text = "";              //テキストUIの中身を消去
        }
    }
}
