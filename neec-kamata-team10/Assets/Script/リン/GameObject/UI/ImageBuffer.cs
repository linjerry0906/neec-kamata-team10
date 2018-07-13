
//------------------------------------------------------
// 作成日：2018.7.13
// 作成者：林 佳叡
// 内容：StageSelectの画像バッファ
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageBuffer : MonoBehaviour 
{
	[SerializeField]
	private FadeState state = FadeState.FadeIn;
	private Image image;
	[SerializeField]
	private float fadeSpeed;
	[SerializeField]
	private Image lockImage;

	private void Start()
	{
		image = GetComponent<Image>();
	}

	private void Update()
	{
		if(state == FadeState.None)
			return;
		
		Fade();
	}

	/// <summary>
	/// Fadeする
	/// </summary>
	private void Fade()
	{
		Color newColor = image.color;
		newColor.a += FadeValue();
		image.color = newColor;
		lockImage.color = newColor * 5;

		if(newColor.a >= 1.0f)
			state = FadeState.None;
		
		if(newColor.a <= 0.0f)				//FadeOut後削除
			Destroy(gameObject);
	}

	/// <summary>
	/// Fade値
	/// </summary>
	/// <returns></returns>
	private float FadeValue()
	{
		if(state == FadeState.FadeIn)
			return fadeSpeed * Time.deltaTime;
		return -fadeSpeed * Time.deltaTime;
	}

	/// <summary>
	/// FadeOutする
	/// </summary>
	public void FadeOut()
	{
		state = FadeState.FadeOut;
	}

	/// <summary>
	/// 画像設定
	/// </summary>
	/// <param name="sprite"></param>
	public void SetSprite(Sprite sprite, bool isLock)
	{
		image = GetComponent<Image>();
		image.sprite = sprite;

		if(!isLock)
			lockImage.enabled = true;
	}
}
