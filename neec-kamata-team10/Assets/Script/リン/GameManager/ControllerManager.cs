//------------------------------------------------------
// 作成日：2018.4.2
// 作成者：林 佳叡
// 内容：コントローラーを管理するクラス
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private KeboardController keyboard_controller;      //キーボードコントローラー
    private PadController pad_controller;               //パッドコントローラー

    public ControllerManager()
    {
        Initialize();                                   //初期化
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Initialize()
    {
        keyboard_controller = new KeboardController();  //キーボード実体生成
        pad_controller = new PadController();           //パッド実体生成
    }

    /// <summary>
    /// キーボードコントローラー取得
    /// </summary>
    /// <returns></returns>
    public ICharacterController Keyboard()
    {
        return keyboard_controller;
    }

    /// <summary>
    /// パッドコントローラーを取得
    /// </summary>
    /// <returns></returns>
    public ICharacterController Pad()
    {
        if(pad_controller.IsConnectedPad())
            return pad_controller;
        return keyboard_controller;
    }
}
