//------------------------------------------------------
// 作成日：2018.7.13
// 作成者：林 佳叡
// 内容：PanelのAlpha値
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class PanelRefAlpha : MonoBehaviour 
{
	[SerializeField]
	private float maxAlpha;
	private Image panelImage;

	void Start () 
	{
		panelImage = GetComponent<Image>();
	}

	public float Alpha()
	{
		return panelImage.color.a;
	}

	public float MaxAlpha()
	{
		return maxAlpha;
	}

	public void SetAlpha(float a)
	{
		Color newColor = panelImage.color;
		newColor.a = a;
		panelImage.color = newColor;
	}
}
