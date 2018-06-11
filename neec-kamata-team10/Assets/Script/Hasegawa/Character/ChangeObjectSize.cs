using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectSize : MonoBehaviour
{
    [SerializeField]
    private float changeTime;             //鏡の範囲外に行ってから元の大きさに戻るまでの時間

    private AudioSource audio;
    private SEManager seManager;
    private SizeEnum sizeStorage;

    private ChangeScale changeScale;
    private Vector3 normalScale;
    //private Rect mirrorRect;
    private bool change = false;
    private float normalMass;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        seManager = GetComponent<SEManager>();
        changeScale = new ChangeScale(new Vector3(1, 1, 1), changeTime);
        //mirrorRect = new Rect(1, 1, 1, 1);
        normalScale = transform.localScale;
        normalMass = GetComponent<Rigidbody>().mass;
        sizeStorage = SizeEnum.Normal;
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

        //ミラーサイズの取得
        changeScale.SetMirrorSize(GetComponent<ObjectSize>().GetReflectSize());
        Vector3 scale = changeScale.Scale(change, size/*mirrorRect, transform.position*/);
        //サイズの変更
        transform.localScale = Vector3.Scale(scale, normalScale);
        //質量の変更
        GetComponent<Rigidbody>().mass = scale.x * scale.y * normalMass;

        //保存したサイズと同じならリターン
        if (size == sizeStorage) return;
        //サイズの保存
        sizeStorage = size;
        //サイズに応じたSEの再生
        PlaySE(size);


        ////ミラーサイズの取得
        //changeScale.SetMirrorSize(GetComponent<ObjectSize>().GetReflectSize());
        //Vector3 scale = changeScale.Scale(change, size/*mirrorRect, transform.position*/);
        ////サイズの変更
        //transform.localScale = Vector3.Scale(scale, normalScale);
        ////質量の変更
        //GetComponent<Rigidbody>().mass = scale.x * scale.y * normalMass;
    }

    //ミラーと衝突していたらchangeScaleにミラーサイズをセットする
    //void OnTriggerEnter(Collider t)
    //{
    //    if (t.gameObject.CompareTag("mirror"))
    //    {
    //        changeScale.SetMirrorSize(t.gameObject.GetComponent<Mirror>().ReflectSize());
    //    }
    //
    //    //if (!t.gameObject.CompareTag("mirror")) return;
    //    //
    //    //Rect r = t.gameObject.GetComponent<Mirror>().GetSide();
    //    //if (changeScale.CheckReflect(r,transform.position))
    //    //{
    //    //    Debug.Log("変形");
    //    //    mirrorRect = r;
    //    //    changeScale.SetMirrorSize(t.gameObject.GetComponent<Mirror>().ReflectSize());
    //    //}
    //}

    public void SetRec(Rect r)
    {
        change = changeScale.CheckReflect(r, transform.position);
        //if (changeScale.CheckReflect(r, transform.position))
        //{
        //    Debug.Log("変形");
        //    mirrorRect = r;
        //}
    }

    private void PlaySE(SizeEnum size)
    {
        if (size == SizeEnum.Normal)
        {
            audio.clip = seManager.GetSE(0);
            audio.Play();
        }
        else if (size <= SizeEnum.Big_XY)
        {
            audio.clip = seManager.GetSE(1);
            audio.Play();
        }
        else
        {
            audio.clip = seManager.GetSE(2);
            audio.Play();
        }
    }
}
