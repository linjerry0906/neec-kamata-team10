//------------------------------------------------------
// 作成日：2018.7.13
// 作成者：林 佳叡
// 内容：UI 文字透明度を指定のObjAlphaで設定
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextRefColor : MonoBehaviour 
{

	[SerializeField]
	private PanelRefAlpha panel;
	private Text targetImage;
	private float maxRate;

	void Start () 
	{
		targetImage = GetComponent<Text>();
		maxRate = 1.0f / panel.MaxAlpha();
	}
	
	void Update () 
	{
		UpdateColor();
	}

	private void UpdateColor()
	{
		Color newColor = targetImage.color;
		newColor.a = panel.Alpha() * maxRate;
		targetImage.color = newColor;
	}
}
