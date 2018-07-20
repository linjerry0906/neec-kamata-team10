
//------------------------------------------------------
// 作成日：2018.7.20
// 作成者：林 佳叡
// 内容：CreditのFader
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CreditFade : MonoBehaviour 
{
	[SerializeField]
	private float fadeTime;
	private Timer timer;
	private bool isFade = false;
	private Image fadeImage;
	private Color maxColor = new Color(0, 0, 0, 1);
	private Color minColor = new Color(0, 0, 0, 0);

	void Start () 
	{
		timer = new Timer(fadeTime);
		fadeImage = GetComponent<Image>();
	}
	
	void Update () 
	{
		if(!isFade)
			return;

		timer.TimeUpdate();
		fadeImage.color = Color.Lerp(minColor, maxColor, timer.Rate() * timer.Rate());
		if(timer.IsTime())
			GameManager.Instance.ChangeScene(EScene.StageSelect);
		
	}

	public void Fade()
	{
		isFade = true;
		timer.Initialize();
	}
}
