//------------------------------------------------------
// 作成日：2018.3.28
// 作成者：林 佳叡
// 内容：鏡のクラス
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField]
    private SizeEnum sizeEnum;                  //サイズEnum
    [SerializeField]
    private Vector3 reflectSize;                //映したサイズ
    [SerializeField]
    private Material reflectMaterial;           //鏡側用のマテリアル
    [SerializeField]
    private Material reflectMagic;
     [SerializeField]
    private Material reflectAppear;
    [SerializeField]
    private Material spriteMaterial;            //鏡側用のマテリアル
    [SerializeField]
    private List<GameObject> originObj;         //映し元
    [SerializeField]
    private List<GameObject> reflectObj;        //映した像
    [SerializeField]
    private string[] tags;

    private Transform reflectParent;            //像の親オブジェクト
    private bool isAlive = true;

    void Start()
    {
        originObj = new List<GameObject>();
        reflectObj = new List<GameObject>();
        isAlive = true;

        GetComponent<MirrorSE>().PlaySetSE();
    }

    private void Update()
    {
        if (!isAlive)
            return;

        for (int i = 0; i < originObj.Count;)                                  //鏡側のObjを修正
        {
            if (reflectObj[i].GetComponent<ReflectObject>().CheckInstance())   //削除されてない場合
            {
                reflectObj[i].GetComponent<ReflectObject>().Reflect();         //位置、サイズ、回転を修正
                ChangeObjectSize cos = originObj[i].GetComponent<ChangeObjectSize>();
                if(cos)
                    cos.SetRec(GetSide());
                ++i;
                continue;
            }
            Destroy(reflectObj[i]);
            originObj.RemoveAt(i);
            reflectObj.RemoveAt(i);
        }
    }

    /// <summary>
    /// 像の親オブジェクトを指定
    /// </summary>
    public void SetReflectParent(Transform parent)
    {
        reflectParent = parent;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("reflect") ||                                 //像は無視
            other.CompareTag("mirror"))                                    //鏡無視
            return;

        if (originObj.Contains(other.gameObject))
            return;

        bool unresizable = IsUnresizableTag(other);

        originObj.Add(other.gameObject);                                   //映したい物を保存
        AddReflectObj(other.gameObject, unresizable);                      //鏡側の像を追加
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectSize>())
            other.GetComponent<ObjectSize>().SetSize(SizeEnum.Normal);
        int index = originObj.IndexOf(other.gameObject);   //削除Indexを確保
        if (index == -1)                                   //他の物
            return;
        if (reflectObj[index])
            Destroy(reflectObj[index]);                    //像のGameObjectを破壊

        originObj.RemoveAt(index);                         //映し元を削除
        reflectObj.RemoveAt(index);                        //鏡側の像を削除
    }

    /// <summary>
    /// サイズ調整できないか
    /// </summary>
    /// <param name="tag">タグ</param>
    /// <returns></returns>
    private bool IsUnresizableTag(Collider collider)
    {
        for (int i = 0; i < tags.Length; ++i)
        {
            if (collider.CompareTag(tags[i]))
                return true;
        }
        return false;
    }

    /// <summary>
    /// 映す像を追加する処理
    /// </summary>
    /// <param name="origin">映し元</param>
    /// <param name="unresizable">大きさ変えられるか</param>
    private void AddReflectObj(GameObject origin, bool unresizable)
    {
        Vector3 dest_size = ReflectSize(unresizable);                       //サイズ指定
        GameObject reflect = Instantiate(origin, reflectParent);            //像のObjectを生成
        DestroyChildCompo(ref reflect);                                     //子供のコンポーネントを削除
        SizeEnum reflectSize = unresizable ? SizeEnum.Normal : sizeEnum;

        reflect.AddComponent<ReflectObject>();                                     //像のコンポーネント追加
        reflect.GetComponent<ReflectObject>().ReflectFrom(origin, dest_size, reflectSize);       //映し元とサイズ設定

        reflectObj.Add(reflect);                                                   //管理リストに追加
    }

    /// <summary>
    /// 子供のコンポーネントを削除
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    private void DestroyChildCompo(ref GameObject obj)
    {
        obj.layer = 8;
        DestroyComponent(ref obj);                                                          //必要がないコンポーネントを削除
        SetReflectMaterial(ref obj);                                                        //マテリアル設定
        for (int i = 0; i < obj.transform.childCount; ++i)                                  //子供全体追加
        {
            GameObject children = obj.transform.GetChild(i).gameObject;                     //子供取得
            DestroyChildCompo(ref children);                                                //再帰
        }
    }

    /// <summary>
    /// Componentを削除
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    private void DestroyComponent(ref GameObject obj)
    {
        foreach (MonoBehaviour m in obj.GetComponents<MonoBehaviour>())       //MonoBehavior
            Destroy(m);

        Rigidbody r = obj.GetComponent<Rigidbody>();                          //rigidbody
        if (r) Destroy(r);
        Collider c = obj.GetComponent<Collider>();                            //Collider
        if (c) Destroy(c);
        Animator a = obj.GetComponent<Animator>();
        if (a) Destroy(a);
    }

    /// <summary>
    /// マテリアル設定
    /// </summary>
    /// <param name="obj">目標オブジェクト</param>
    private void SetReflectMaterial(ref GameObject obj)
    {
        MeshRenderer mesh = obj.GetComponent<MeshRenderer>();                 //モデル
        SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();           //Sprite
        if (mesh)                                                             //メッシュレンダラーがある場合
        {
            int length = mesh.materials.Length;
            Material[] materials = new Material[length];
            for (int i = 0; i < length; ++i)
            {
                Texture mTex = mesh.materials[i].mainTexture;                 //テクスチャ取得
                materials[i] = GetReflectMaterial(obj.tag);                          //マテリアル設定
                materials[i].SetTexture("_MainTex", mTex);                    //テクスチャ設定
                materials[i].SetColor("_Color", mesh.materials[i].color);
                Texture mEmiss = mesh.materials[i].GetTexture("_EmissionMap");
                Color mEColor = mesh.materials[i].GetColor("_EmissionColor");
                materials[i].SetTexture("_EmissionMap", mEmiss);
                materials[i].SetColor("_EmissionColor", mEColor);
            }
            obj.tag = "reflect";                                                  //Tag修正
            mesh.materials = materials;
        }
        if (sprite)                                                           //スプライトがある場合
        {
            Texture sTex = sprite.material.mainTexture;                       //テクスチャ取得
            sprite.material = spriteMaterial;                                 //マテリアル設定
            sprite.material.SetTexture("_MainTex", sTex);                     //テクスチャ設定
        }
    }

    /// <summary>
    /// 映すサイズ
    /// </summary>
    /// <param name="unresizable">大きさ変えられるか</param>
    /// <returns></returns>
    private Vector3 ReflectSize(bool unresizable)
    {
        if (unresizable)                                   //変わりたくないObjの場合
            return new Vector3(1, 1, 1);                   //サイズ変更なし
        return reflectSize;                                //プリセットのサイズ
    }

    /// <summary>
    /// 映すサイズ
    /// </summary>
    /// <returns></returns>
    public Vector3 ReflectSize()
    {
        return reflectSize;                                //プリセットのサイズ
    }

    /// <summary>
    /// 鏡の各辺の座標、サイズ
    /// </summary>
    /// <returns></returns>
    public Rect GetSide()
    {
        float xScale = GetComponent<BoxCollider>().size.x / 2 * transform.localScale.x;     //Width / 2
        float yScale = GetComponent<BoxCollider>().size.y / 2 * transform.localScale.y;     //Height / 2
        float x = transform.position.x - xScale;                                            //左上のX座標
        float y = transform.position.y - yScale;                                            //左上のY座標
        return new Rect(x, y, xScale * 2, yScale * 2);
    }

    private void OnDestroy()
    {
        Release();
        DestroyReflects();                      //像を消す
    }

    /// <summary>
    /// 像の情報などをリリース
    /// </summary>
    public void Release()
    {
        DestroyReflects();                      //像を消す

        for (int i = 0; i < originObj.Count; ++i)
        {
            if (!originObj[i])
                continue;
            ObjectSize objSize = originObj[i].GetComponent<ObjectSize>();
            if (objSize)
                objSize.SetSize(SizeEnum.Normal);

            ChangeObjectSize cos = originObj[i].GetComponent<ChangeObjectSize>();
            if (cos)
                cos.ReleaseMirror();
        }
        originObj.Clear();                      //リストクリア
    }

    /// <summary>
    /// 像を消す
    /// </summary>
    private void DestroyReflects()
    {
        for (int i = 0; i < reflectObj.Count; ++i)
        {
            Destroy(reflectObj[i]);
        }
        reflectObj.Clear();
    }

    /// <summary>
    /// 鏡を割る
    /// </summary>
    public void DestroyMirror()
    {
        isAlive = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MirrorSE>().PlayBreakSE();
        Release();
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(6).gameObject.SetActive(true);
        Destroy(gameObject, 0.7f);
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    private Material GetReflectMaterial(string tag)
    {
        switch(tag)
        {
            case "magic_block":
                return reflectMagic;
            case "appear_block":
                return new Material(reflectAppear);
            default:
                return reflectMaterial;
        }
    }
}
