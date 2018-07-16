//------------------------------------------------------
// 作成日：2018.7.16
// 作成者：林 佳叡
// 内容：クレジット画面の管理者
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour 
{
	private ICharacterController controller;

	void Start () 
	{
		controller = GameManager.Instance.GetController();
	}
	
	void Update () 
	{
		
	}

	private void BackButton()
	{
		if(controller.OperateTheMirror() || controller.Jump())
			GameManager.Instance.ChangeScene(EScene.StageSelect);
	}
}
