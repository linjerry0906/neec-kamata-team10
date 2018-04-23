﻿//------------------------------------------------------
// 作成日：2018.4.2
// 作成者：林 佳叡
// 内容：ゲームマネージャー
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;                     //GameManagerのインスタンス

    private ControllerManager controller_manager;           //コントローラーのマネージャー

    private void Awake()
    {
        CheckInstance();                                    //Instanceをチェックする

        controller_manager = new ControllerManager();       //実体生成
    }

    /// <summary>
    /// Instanceをチェックする
    /// </summary>
    private void CheckInstance()
    {
        if (Instance != null)                               //Nullじゃない場合
        {
            Destroy(this.gameObject);                       //削除
            return;
        }
        Instance = this;                                    //Instance指定
        DontDestroyOnLoad(this.gameObject);                 //削除されないように
    }

    void Start () {
		
	}
	
	void Update () {
		
	}

    /// <summary>
    /// 指定のコントローラーを取得
    /// </summary>
    /// <param name="eController">キーボードか、パッドか</param>
    /// <returns></returns>
    public ICharacterController GetController(EController eController)
    {
        if(eController == EController.KEYBOARD)             //キーボードの場合
            return controller_manager.Keyboard();           //キーボードのコントローラーを返す

        return controller_manager.Pad();                    //パッドのコントローラーを返す
    }
}
