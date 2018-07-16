//------------------------------------------------------
// 作成日：2018.7.13
// 作成者：林 佳叡
// 内容：StageSelect背景設定
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectBackground : MonoBehaviour 
{
	[SerializeField]
	private Sprite[] stageImage;
	[SerializeField]
	private Sprite creditImage;
	[SerializeField]
	private GameObject imageBuffer;
	private int currentIndex = -1;
	[SerializeField]
	private List<GameObject> buffers;

	/// <summary>
	/// 背景設定
	/// </summary>
	/// <param name="stage"></param>
	public void SetBackGround(int stage, bool isLock)
	{
		Sprite background =  GetSprite(stage);

		if(stage == currentIndex)				//同じの場合は変更しない
			return;

		currentIndex = stage;
		GameObject buffer = Instantiate(imageBuffer, transform);
		buffer.GetComponent<ImageBuffer>().SetSprite(background, isLock);
		FadeOutAll();
		buffers.Add(buffer);
	}

	/// <summary>
	/// スプライトを取得
	/// </summary>
	/// <param name="stage">ステージ数</param>
	/// <returns></returns>
	private Sprite GetSprite(int stage)
	{
		if(stage == (int)EScene.Credit)
		{
			return creditImage;
		}

		int index = stage - 1;					//ステージから添え字
		index = Mathf.Max(0, Mathf.Min(index, stageImage.Length - 1));
		return stageImage[index];
	}

	private void FadeOutAll()
	{
		buffers.RemoveAll(b => !b);
		for(int i = 0; i < buffers.Count; ++i)
		{
			buffers[i].GetComponent<ImageBuffer>().FadeOut();
		}
	}
}
