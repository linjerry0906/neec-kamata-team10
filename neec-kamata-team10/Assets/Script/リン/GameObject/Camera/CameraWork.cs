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
    private TraceMode mode;                     //追尾モード
    [SerializeField]
    private bool isSmooth = false;              //等速追尾（静止オブジェクト）
    [SerializeField]
    private GameObject target;                  //注目先
    [SerializeField]
    private float dampTime;                     //移動にかかる時間

    private Vector3 velocity = Vector3.zero;    //Velocity
    private Camera camera;                      //カメラ

    [SerializeField]
    private Vector3 relativeVec = new Vector3(0, 6, -17);       //相対位置

    void Start ()
    {
        camera = GetComponent<Camera>();
	}
	
	void Update ()
    {
        if (isSmooth)
        {
            SmoothLerp();
            return;
        }

        Trace();
    }

    private void SmoothLerp()
    {
        if (target)
        {
            Vector3 targetPos = target.transform.position;
            Vector3 point = camera.WorldToViewportPoint(targetPos);
            Vector3 direct = targetPos - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + direct;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }

    private void Trace()
    {
        Vector3 dest = target.transform.position + relativeVec;
        Vector3 dir = dest - transform.position;

        float length = dir.magnitude;
        if (length < 3)
            return;

        dir /= length;
        transform.position += dir * (length - 3) * 0.01f;
    }
}
