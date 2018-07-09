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
	bool Pause();					  //一時停止:

    bool MoveSelectionUp();           //選択項目を上に移動
    bool MoveSelectionDown();         //選択項目を下に移動
    bool MoveSelectionLeft();         //選択項目を左に移動
    bool MoveSelectionRight();        //選択項目を右に移動
    void Update();                    //選択状態の更新
    bool GameEnd();                   //ゲームを終了

    void CountFlameInit();            //フレームカウント数を初期化する
    void CountFlameUpdate();          //押しているフレーム数を数える
    int GetFlameCount();              //フレーム数を取得する
    bool IsFade();                    //シーンがフェードしたか
    void SetFadeFlag(bool isFade);    //シーンフェードフラグの設定
}
