//------------------------------------------------------
// 作成日：2018.5.18
// 作成者：林 佳叡
// 内容：アニメーションステートインターフェース
//------------------------------------------------------
using UnityEngine;

public interface IAnimeState
{
    void Execute(Animator animator);        //実行
    void Exit(Animator animator);           //状態変更の前にやっておきたいこと
    void Update();                          //更新処理
    bool IsEnd();                           //終わっているか
}
