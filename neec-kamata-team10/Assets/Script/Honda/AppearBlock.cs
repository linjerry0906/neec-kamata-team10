using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作成日   2018/6/9
/// 最終更新 2018/6/11
/// 作成者   本田尚大
/// 
/// 使い方
///     Objectの Active = false にするとコードまで無効になるので
///     Emptyにこのコードを持たせてEmptyのChildとして対象のObjectを持たせてください。
///     UnityのGameObject.Activeを切り替えているのでObjectならなんでも操作できるはず。
///     
/// フェード周りの仕様
///     ・完全にFadeOutするまでは(Unityの仕様的にも)Activeにしておく。
///     
/// </summary>

public class AppearBlock : AppearBlockBase
{
    [SerializeField]
    private new GameObject appearObject; //スイッチで切り替えさせるObject
    [SerializeField]
    private new bool IsReverseAppear = false; //trueだとスイッチOnで消滅、スイッチOffで出現

    [SerializeField]
    public SwitchObject switchObj; //スイッチ本体(のコード)


    //FadeTime関連
    [SerializeField]
    private new float fadeTime = 0.2f;  //fade時間

    // Use this for initialization
    void Start()
    {
        base.appearObject = this.appearObject;
        base.IsReverseAppear = this.IsReverseAppear;
        base.fadeTime = this.fadeTime;

        base.OriginStart();
    }

    void Update()
    {
        //現在の状態と新しい状態を比較 違う=XORがtrue
        base.newActive = switchObj.IsTurnOn ^ IsReverseAppear;

        base.OriginUpdate();
    }
}
