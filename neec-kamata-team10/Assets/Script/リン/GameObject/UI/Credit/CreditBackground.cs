//------------------------------------------------------
// 作成日：2018.7.20
// 作成者：林 佳叡
// 内容：Creditの背景
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CreditBackground : MonoBehaviour 
{
	[SerializeField]
	private RectTransform scrollPanel;
	[SerializeField]
	private float scrollMin;
	[SerializeField]
	private float scrollMax;
	[SerializeField]
	private float totalTime;
	private Timer timer;
	private int totalCount;
	private int currentCount = 0;
	[SerializeField]
	private CreditFade creditFade;
	[SerializeField]
	private Sprite[] background;
	private Image backgroundImage;
	[SerializeField]
	private Color maxAlpha;
	[SerializeField]
	private Color minAlpha;
	private bool isEnd = false;

	void Start () 
	{
		backgroundImage = GetComponent<Image>();
		totalCount = background.Length;
		timer = new Timer(totalTime / totalCount);
		timer.Initialize();
	}
	

	void Update () 
	{
		if(isEnd)
			return;

		UpdateTimer();
		UpdateBackground();
		UpdateCredit();
	}
	
	/// <summary>
	/// タイマー更新
	/// </summary>
	private void UpdateTimer()
	{
		timer.TimerUpdate();
		if(timer.IsTime())
		{
			++currentCount;
			IsEnd();

			if(isEnd)
				return;
			SetImage();
			timer.Initialize();
		}
	}

	/// <summary>
	/// 終了確認
	/// </summary>
	private void IsEnd()
	{
		if(currentCount >= totalCount)
		{
			isEnd = true;
			creditFade.Fade();
		}
	}

	/// <summary>
	/// 画像設定
	/// </summary>
	private void SetImage()
	{
		currentCount = Mathf.Clamp(currentCount, 0, totalCount - 1);
		backgroundImage.sprite = background[currentCount];
	}

	/// <summary>
	/// 背景画像更新
	/// </summary>
	private void UpdateBackground()
	{
		backgroundImage.color = GetColor();
	}

	/// <summary>
	/// 画像色を取得
	/// </summary>
	/// <returns></returns>
	private Color GetColor()
	{
		float rate = 1 - Mathf.Abs(2 * timer.Rate() - 1);
		return Color.Lerp(minAlpha, maxAlpha, rate * 3);
	}

	private void UpdateCredit()
	{
		Vector2 pos = scrollPanel.anchoredPosition;
		float rate = currentCount * 1.0f / totalCount + (timer.Rate() / totalCount);
		pos.y = Mathf.Lerp(scrollMin, scrollMax, rate);
		scrollPanel.anchoredPosition = pos;
	}
}
