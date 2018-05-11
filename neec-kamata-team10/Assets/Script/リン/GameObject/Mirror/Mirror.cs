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
    private Material spriteMaterial;            //鏡側用のマテリアル
    [SerializeField]
    private List<GameObject> originObj;         //映し元
    [SerializeField]
    private List<GameObject> reflectObj;        //映した像

    private bool isHand = false;

    void Start()
    {
        originObj = new List<GameObject>();
        reflectObj = new List<GameObject>();
    }

    private void Update()
    {
        for (int i = 0; i < originObj.Count;)                              //鏡側のObjを修正
        {
            if (reflectObj[i].GetComponent<ReflectObject>().CheckInstance())   //削除されてない場合
            {
                bool hand = IsUnresizableTag(originObj[i].tag) ? false : isHand;
                reflectObj[i].GetComponent<ReflectObject>().Reflect(isHand);   //位置、サイズ、回転を修正
                ++i;
                continue;
            }
            Destroy(reflectObj[i]);
            originObj.RemoveAt(i);
            reflectObj.RemoveAt(i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("reflect") ||                                 //像は無視
            other.tag.Equals("mirror"))                                    //鏡無視
            return;

        bool unresizable = IsUnresizableTag(other.tag);
        if (isHand && !unresizable)
            return;

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
    private bool IsUnresizableTag(string tag)
    {
        if (tag.Equals("Unresizable"))
            return true;
        if (tag.Equals("stage_block"))
            return true;
        if (tag.Equals("Player"))
            return true;
        return false;
    }

    /// <summary>
    /// 映す像を追加する処理
    /// </summary>
    /// <param name="origin">映し元</param>
    /// <param name="unresizable">大きさ変えられるか</param>
    private void AddReflectObj(GameObject origin, bool unresizable)
    {
        Vector3 dest_size = ReflectSize(unresizable);       //サイズ指定
        GameObject reflect = Instantiate(origin);           //像のObjectを生成
        DestroyChildCompo(ref reflect);                     //子供のコンポーネントを削除
        SizeEnum reflectSize = unresizable ? SizeEnum.Normal : sizeEnum;

        reflect.AddComponent<ReflectObject>();                                      //像のコンポーネント追加
        reflect.GetComponent<ReflectObject>().SetMirror(gameObject);
        reflect.GetComponent<ReflectObject>().ReflectFrom(origin, dest_size, reflectSize);       //映し元とサイズ設定
        reflect.GetComponent<ReflectObject>().Reflect(false);                      //映す

        reflectObj.Add(reflect);                           //管理リストに追加
    }

    /// <summary>
    /// 子供のコンポーネントを削除
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    private void DestroyChildCompo(ref GameObject obj)
    {
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
        foreach (MonoBehaviour m in obj.GetComponents<MonoBehaviour>())
        {
            Destroy(m);
        }

        Rigidbody r = obj.GetComponent<Rigidbody>();
        if (r) Destroy(r);
        Collider c = obj.GetComponent<Collider>();
        if (c) Destroy(c);
    }

    /// <summary>
    /// マテリアル設定
    /// </summary>
    /// <param name="obj">目標オブジェクト</param>
    private void SetReflectMaterial(ref GameObject obj)
    {
        obj.tag = "reflect";                                                  //Tag修正
        MeshRenderer mesh = obj.GetComponent<MeshRenderer>();                 //モデル
        SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();           //Sprite
        if (mesh)                                                             //メッシュレンダラーがある場合
        {
            Texture mTex = mesh.material.mainTexture;                         //テクスチャ取得
            mesh.material = reflectMaterial;                                  //マテリアル設定
            mesh.material.SetTexture("_MainTex", mTex);                       //テクスチャ設定
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

    /// <summary>
    /// 手に持っているか
    /// </summary>
    /// <param name="isHand">手に持っているか</param>
    public void SetHand(bool isHand)
    {
        this.isHand = isHand;
        BindObject();
    }

    private void BindObject()
    {
        for(int i = 0; i < originObj.Count; ++i)
        {
            if (IsUnresizableTag(originObj[i].tag))
                continue;

            reflectObj[i].GetComponent<ReflectObject>().SetMirror(gameObject);
            reflectObj[i].GetComponent<ReflectObject>().Reflect(isHand);
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject g in reflectObj)
        {
            Destroy(g);
        }
        reflectObj.Clear();
    }
}
