//------------------------------------------------------
// 作成日：2018.7.9
// 作成者：林 佳叡
// 内容：ステージ選択からタイトルへ戻る
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectBack : MonoBehaviour {

	private GameManager gameManager;
	private ICharacterController controller;

	private SystemSE systemSE;

	void Start () 
	{
		gameManager = GameManager.Instance;
		systemSE = gameManager.GetSystemSE();
		controller = gameManager.GetController();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(controller.OperateTheMirror())
		{
			gameManager.ChangeScene(EScene.Title);
			systemSE.PlaySystemSE(SystemSoundEnum.se_cancel);
		}
	}
}
