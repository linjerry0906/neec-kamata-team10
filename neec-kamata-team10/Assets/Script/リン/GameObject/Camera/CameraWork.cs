//------------------------------------------------------
// 作成日：2018.4.13
// 作成者：林 佳叡
// 内容：カメラワーク
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour {

    [SerializeField]
    private TraceMode currentMode;                              //追尾モード
    [SerializeField]
    private GameObject target;                                  //注目先
    [SerializeField]
    private float dampTime;                                     //移動にかかる時間
    [SerializeField]
    private Vector3 relativePos = new Vector3(0, 14, -30);      //相対位置
    [SerializeField]
    private float radius;                                       //移動しない半径
    [SerializeField]
    private float charaTraceSpeed;                              //キャラ追尾するスピード

    private CameraMode cameraMode;                              //動くモード
    private TraceMode previousMode;                             //Debug：前回のモード

    void Start ()
    {
        previousMode = TraceMode.TRACE_CHARACTER;               //Debug：前回のモードを初期化
        cameraMode = ModeFactory(previousMode);                 //指定モードを生成
	}
	
	void Update ()
    {
        if (previousMode != currentMode)                        //Debug：変更があれば
        {
            ChangeMode(currentMode);                            //Debug：現在のモードに変更
            previousMode = currentMode;                         //Debug：前回のモードを更新
        }

        cameraMode.SetTarget(target);                           //Debug：ターゲット指定
        cameraMode.Trace();                                     //追尾する
    }

    /// <summary>
    /// 追尾モードを変更
    /// </summary>
    /// <param name="mode">モード</param>
    private void ChangeMode(TraceMode mode)
    {
        cameraMode = ModeFactory(mode);                         //ファクトリーから作成
    }

    /// <summary>
    /// 注目ターゲットを変更
    /// </summary>
    /// <param name="target">ターゲット</param>
    public void SetTarget(GameObject target)
    {
        this.target = target;               //変更
        cameraMode.SetTarget(target);       //設定
    }

    /// <summary>
    /// カメラモードのファクトリー
    /// </summary>
    /// <param name="mode">カメラモード</param>
    /// <returns></returns>
    private CameraMode ModeFactory(TraceMode mode)
    {
        switch (mode)
        {
            case TraceMode.TRACE_CHARACTER:
                return new TraceCharacter(gameObject, target, relativePos, charaTraceSpeed, radius);     //キャラクターを追尾

            case TraceMode.TRACE_HINT:
                return new TraceHint(target, gameObject, dampTime);                                      //指定オブジェクトを追尾
        }

        return new TraceCharacter(gameObject, target, relativePos, charaTraceSpeed, radius);             //デフォルト
    }
}
