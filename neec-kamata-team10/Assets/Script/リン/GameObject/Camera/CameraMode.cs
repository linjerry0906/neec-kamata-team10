//------------------------------------------------------
// 作成日：2018.4.13
// 作成者：林 佳叡
// 内容：カメラ追尾モードのインターフェース
//------------------------------------------------------
using UnityEngine;

public interface CameraMode
{
    /// <summary>
    /// 追尾
    /// </summary>
    void Trace();
    /// <summary>
    /// ターゲットを設定
    /// </summary>
    /// <param name="target">ターゲット</param>
    void SetTarget(GameObject target);
}
