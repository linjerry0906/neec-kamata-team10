//------------------------------------------------------
// 作成日：2018.7.13
// 作成者：林 佳叡
// 内容：UI Image透明度を指定のObjAlphaで設定
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageRefColor : MonoBehaviour
 {
	[SerializeField]
	private PanelRefAlpha panel;
	private Image targetImage;
	private float maxRate;

	void Start () 
	{
		targetImage = GetComponent<Image>();
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
