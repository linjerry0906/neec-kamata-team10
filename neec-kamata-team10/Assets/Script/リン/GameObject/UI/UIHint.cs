//------------------------------------------------------
// 作成日：2018.7.13
// 作成者：林 佳叡
// 内容：UIで操作ヒントを表示する
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHint : MonoBehaviour 
{
	private PanelRefAlpha panel;
	private ICharacterController controller;
	private bool fadeIn = true;
	[SerializeField]
	private float fadeSpeed;
	[SerializeField]
	private float waitTime;
	private Timer timer;

	void Start () 
	{
		panel = GetComponent<PanelRefAlpha>();
		controller = GameManager.Instance.GetController();

		timer = new Timer(waitTime);
	}

	void Update () 
	{
		SwitchFade();
		UpdateTimer();
		UpdatePanelAlpha();
	}

	private void SwitchFade()
	{
		float xMov = Mathf.Abs(controller.HorizontalMove().x);
		float yMov = Mathf.Abs(controller.HorizontalMove().y);

		if(xMov > 0.0f || yMov > 0.0f)
		{
			fadeIn = false;
			timer.Initialize();
		}
	}

	/// <summary>
	/// ヒントタイマー更新
	/// </summary>
	private void UpdateTimer()
	{
		if(fadeIn)
			return;

		timer.TimerUpdate();
		if(timer.IsTime())
			fadeIn = true;
	}

	/// <summary>
	/// パネルのAlphaを設定
	/// </summary>
	private void UpdatePanelAlpha()
	{
		float current = panel.Alpha();

		current+=FadeValue();
		current = Mathf.Max(0, Mathf.Min(current, panel.MaxAlpha()));
		panel.SetAlpha(current);
	}

	/// <summary>
	/// FadeValueを取得
	/// </summary>
	/// <returns></returns>
	private float FadeValue()
	{
		if(fadeIn)
			return fadeSpeed * Time.deltaTime;
		return -fadeSpeed * Time.deltaTime;
	}
}
