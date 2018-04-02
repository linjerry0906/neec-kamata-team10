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
        return pad_controller;
    }
}
