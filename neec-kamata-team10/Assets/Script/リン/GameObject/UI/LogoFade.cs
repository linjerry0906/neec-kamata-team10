//------------------------------------------------------
// 作成日：2018.7.9
// 作成者：林 佳叡
// 内容：Logoのフェイド制御
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoFade : MonoBehaviour {

	[SerializeField]
	private Image fadeImage;
	[SerializeField]
	private float fadeSecond;
	private Timer timer;

	void Start () 
	{
		timer = new Timer(fadeSecond);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer.TimeUpdate();

		SetFadeColor();

		if(timer.IsTime())								//時間に来たらTitleシーン
		{
			GameManager.Instance.ChangeScene(EScene.Title);
		}
	}

	/// <summary>
	/// フェイド色設定
	/// </summary>
	private void SetFadeColor()
	{
		float rate = timer.Rate();
		float alpha = Mathf.Abs((rate * 2) - 1);		//1＞0＞1

		Color fadeColor = fadeImage.color;				//色取得
		fadeColor.a = alpha * alpha;					//Alpha設定
		fadeImage.color = fadeColor;					//色設定
	}
}
