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
    private Vector3 relativePos = new Vector3(0, 6, -17);       //相対位置
    [SerializeField]
    private float radius;                                       //移動しない半径

    private CameraMode cameraMode;                              //動くモード
    private TraceMode previousMode;

    void Start ()
    {
        previousMode = TraceMode.TRACE_CHARACTER;
        cameraMode = new TraceCharacter(gameObject, target, relativePos, radius);
	}
	
	void Update ()
    {
        if (previousMode != currentMode)
        {
            ChangeMode(currentMode);
            previousMode = currentMode;
        }

        cameraMode.SetTarget(target);
        cameraMode.Trace();
    }

    private void ChangeMode(TraceMode mode)
    {
        if (mode == TraceMode.TRACE_CHARACTER)
        {
            cameraMode = new TraceCharacter(gameObject, target, relativePos, radius);
            return;
        }

        cameraMode = new TraceHint(target, gameObject, dampTime);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
        cameraMode.SetTarget(target);
    }
}
