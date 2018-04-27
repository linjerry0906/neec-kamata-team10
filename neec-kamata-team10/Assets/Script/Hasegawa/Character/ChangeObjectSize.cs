using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectSize : MonoBehaviour {
    [SerializeField]
    private float changeTime;             //鏡の範囲外に行ってから元の大きさに戻るまでの時間

    private ChangeScale changeScale;

    // Use this for initialization
    void Start()
    {
        changeScale = new ChangeScale(new Vector3(1, 1, 1), changeTime);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScale();
    }

    //スケールの変更
    void ChangeScale()
    {
        if (tag == "reflect")
            return;
        SizeEnum size = GetComponent<ObjectSize>().GetSize();
        transform.localScale = changeScale.Scale(size);
    }

    //ミラーと衝突していたらchangeScaleにミラーサイズをセットする
    void OnTriggerEnter(Collider t)
    {
        if (t.gameObject.CompareTag("mirror"))
        {
            changeScale.SetMirrorSize(t.gameObject.GetComponent<Mirror>().ReflectSize());
        }
    }
}
