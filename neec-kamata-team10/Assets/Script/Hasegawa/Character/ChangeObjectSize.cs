using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectSize : MonoBehaviour
{
    [SerializeField]
    private float changeTime;             //鏡の範囲外に行ってから元の大きさに戻るまでの時間
    [SerializeField]
    private float lerpLate = 0.1f;

    private float lerpTime = 0;

    private AudioSource audio;
    private SEManager seManager;
    private SizeEnum size;
    private SizeEnum sizeStorage;

    private ChangeScale changeScale;
    private Vector3 normalScale;
    private Vector3 scale;
    private Vector3 scaleStorage;
    //private Rect mirrorRect;
    private bool hitMirror = false;
    private bool hitMirrorStorage = false;
    private float normalMass;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        seManager = GetComponent<SEManager>();
        changeScale = new ChangeScale(new Vector3(1, 1, 1), changeTime);
        //mirrorRect = new Rect(1, 1, 1, 1);
        normalScale = transform.localScale;
        scale = new Vector3(1, 1, 1);//transform.localScale;
        scaleStorage = scale;
        normalMass = GetComponent<Rigidbody>().mass;
        sizeStorage = SizeEnum.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "reflect") return;

        ChangeScale();

        //保存したサイズと同じならリターン
        if (size == sizeStorage) return;
        //サイズの保存
        sizeStorage = size;
        //hitMirrorの保存
        hitMirrorStorage = hitMirror;
        //サイズに応じたSEの再生
        PlaySE(size);
    }

    //スケールの変更
    void ChangeScale()
    {
        size = GetComponent<ObjectSize>().GetSize();

        //ミラーサイズの取得
        changeScale.SetMirrorSize(GetComponent<ObjectSize>().GetReflectSize());

        //////
        Vector3 targetScale = changeScale.Scale(hitMirror, size);
        scale = Lerp(scale, targetScale);
        //////

        //Vector3 targetScale = changeScale.Scale(hitMirror, size);
        //scale = Lerp2(scaleStorage, targetScale);

        //Vector3 scale = changeScale.Scale(hitMirror, size/*mirrorRect, transform.position*/);
        //サイズの変更
        transform.localScale = Vector3.Scale(scale, normalScale);
        //transform.localScale = Vector3.Scale(scale, normalScale);
        //質量の変更
        GetComponent<Rigidbody>().mass = scale.x * scale.y * normalMass;

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
        hitMirror = changeScale.CheckReflect(r, transform.position);
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
            //Debug.Log("normalSE");
        }
        else if (size <= SizeEnum.Big_XY)
        {
            audio.clip = seManager.GetSE(1);
            audio.Play();
            //Debug.Log("BigSE");
        }
        else
        {
            audio.clip = seManager.GetSE(2);
            audio.Play();
            //Debug.Log("SmallSE");
        }
    }

    private Vector3 Lerp(Vector3 scale, Vector3 targetScale)
    {
        if (scale == targetScale) return scale;
        //if (size == sizeStorage) return new Vector3(1,1,1);
        //if (lerpTime >= 1.0f) lerpTime = 0;

        if (Mathf.Approximately(scale.x, targetScale.x) &&
            Mathf.Approximately(scale.y, targetScale.y))
        {
            return targetScale;
        }
        else
        {
            return Vector3.Lerp(scale, targetScale, 10f * Time.deltaTime/*lerpTime*/);
        }
    }

    private Vector3 Lerp2(Vector3 scale, Vector3 targetScale)
    {
        //Debug.Log(lerpTime);
        if (scale == targetScale) { lerpTime = 0; return scale; }
        //if (size == sizeStorage) return new Vector3(1,1,1);
        //if (lerpTime >= 1.0f) lerpTime = 0;

        if (lerpTime>=1.0f)
        {
            //Debug.Log("初期化");
            //lerpTime = 0;
            scaleStorage = scale;
            return targetScale;
        }
        //if (Mathf.Approximately(scale.x, targetScale.x) &&
        //    Mathf.Approximately(scale.y, targetScale.y))
        //{
        //    lerpTime = 0;
        //    scaleStorage = scale;
        //    return targetScale;
        //}
        else
        {
            lerpTime += Time.deltaTime * 3.0f;
            return Vector3.Lerp(scale, targetScale, lerpTime);
        }
    }

    //private void Scale()
    //{
    //    Vector3 targetScale = Vector3.Scale(normalScale, changeScale.Scale(hitMirror, size));
    //    scale = Lerp(scale, targetScale);
    //}

    //6.15 本田 変更:鏡消去時のrelease処理を追加
    public void ReleaseMirror()
    {
        hitMirror = false;
    }
}
