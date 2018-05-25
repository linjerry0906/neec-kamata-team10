using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController 
{
    Vector3 HorizontalMove();         //水平移動
    Vector3 VerticalMove();
    bool Jump();                      //ジャンプ
    bool SwitchToTheRight();          //鏡を右に切り替える
    bool SwitchToTheLeft();           //鏡を左に切り替える
    bool OperateTheMirror();          //鏡の操作
    bool ThrowMirror();               //鏡を投げる（キーを離したとき:true）
    void CountFlameInit();            //フレームカウント数を初期化する
    void CountFlameUpdate();          //押しているフレーム数を数える
    int GetFlameCount();              //フレーム数を取得する

    bool IsFade();                    //シーンがフェードしたか
    void SetFadeFlag(bool isFade);    //シーンフェードフラグの設定


}
