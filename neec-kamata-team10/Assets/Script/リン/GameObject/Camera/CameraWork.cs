//------------------------------------------------------
// 作成日：2018.4.13
// 作成者：林 佳叡
// 内容：カメラワーク
//------------------------------------------------------
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
    private float charaTraceSpeed = 7;                          //キャラ追尾するスピード
    [SerializeField]
    private Vector2 minPos = new Vector2(-1000, -100);
    [SerializeField]
    private Vector2 maxPos = new Vector2(1000, 500);

    private CameraMode cameraMode;                              //動くモード

    void Start ()
    {
        Restart();
        cameraMode = ModeFactory(currentMode);                  //指定モードを生成
	}

    /// <summary>
    /// 再開の処理
    /// </summary>
    private void Restart()
    {
        Vector3 startPos = GameManager.Instance.GetStageManager().CameraPos();
        if (startPos == Vector3.zero)                           //再開じゃない場合は以下実行しない
            return;

        transform.position = startPos;
        Time.timeScale = 1;                                     //タイムを正常
        GameManager.Instance.GetStageManager().StartStage();               //Time計算開始
    }
	
	void Update ()
    {
        cameraMode.Trace();                                     //追尾する
        cameraMode.Clamp(minPos, maxPos);                       //座標クランプ
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
        cameraMode = ModeFactory(currentMode);
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
