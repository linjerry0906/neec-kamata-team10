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

    void Start()
    {
        originObj = new List<GameObject>();
        reflectObj = new List<GameObject>();
    }

    private void Update()
    {
        for (int i = 0; i < originObj.Count; i++)                          //鏡側のObjを修正
        {
            reflectObj[i].GetComponent<ReflectObject>().Reflect();         //位置、サイズ、回転を修正
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("reflect") ||                                 //像は無視
            other.tag.Equals("mirror"))                                    //鏡無視
            return;

        originObj.Add(other.gameObject);                                   //映したい物を保存
        bool unresizable = IsUnresizableTag(other.tag);
        AddReflectObj(other.gameObject, unresizable);                      //鏡側の像を追加
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectSize>())
            other.GetComponent<ObjectSize>().SetSize(SizeEnum.Normal);
        int index = originObj.IndexOf(other.gameObject);   //削除Indexを確保
        if(reflectObj[index])
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
        Destroy(reflect.GetComponent<Collider>());          //必要がないコンポーネントを削除
        reflect.tag = "reflect";                            //Tag追加
        for (int i = 0; i < reflect.transform.childCount; ++i)                      //子供全体追加
        {
            Transform child = reflect.transform.GetChild(i);
            child.tag = "reflect";
            MeshRenderer mesh = child.GetComponent<MeshRenderer>();                 //モデル
            SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();           //Sprite
            if (mesh)
                mesh.material = reflectMaterial;
            if (sprite)
                sprite.material = spriteMaterial;
        }

        SizeEnum reflectSize = unresizable ? SizeEnum.Normal : sizeEnum;
        reflect.GetComponent<MeshRenderer>().material = reflectMaterial;            //マテリアル設定
        reflect.AddComponent<ReflectObject>();                                      //像のコンポーネント追加
        reflect.GetComponent<ReflectObject>().ReflectFrom(origin, dest_size, reflectSize);       //映し元とサイズ設定
        reflect.GetComponent<ReflectObject>().Reflect();                            //映す

        reflectObj.Add(reflect);                           //管理リストに追加
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
}
