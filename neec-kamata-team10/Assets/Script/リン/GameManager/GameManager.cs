﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private ControllerManager controller_manager;           //コントローラーのマネージャー

    private void Awake()
    {
        controller_manager = new ControllerManager();
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
