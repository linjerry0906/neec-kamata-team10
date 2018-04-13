//------------------------------------------------------
// 作成日：2018.3.28
// 作成者：林 佳叡
// 内容：鏡のクラス
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {

    [SerializeField]
    private Vector3 reflect_size;               //映したサイズ
    [SerializeField]
    private Material mirror_obj_material;       //鏡側用のマテリアル
    [SerializeField]
    private List<GameObject> origin_obj;        //映し元
    [SerializeField]
    private List<GameObject> reflect_obj;       //映した像

	void Start ()
    {
        origin_obj = new List<GameObject>();
        reflect_obj = new List<GameObject>();
	}

    private void Update()
    {
        for (int i = 0; i < origin_obj.Count; i++)                          //鏡側のObjを修正
        {
            reflect_obj[i].GetComponent<ReflectObject>().Reflect();         //位置、サイズ、回転を修正
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("reflect"))                                    //像は無視
            return;

        origin_obj.Add(other.gameObject);                                   //映したい物を保存
        AddReflectObj(other.gameObject, other.tag.Equals("Unresizable"));   //鏡側の像を追加
    }

    private void OnTriggerExit(Collider other)
    {
        int index = origin_obj.IndexOf(other.gameObject);   //削除Indexを確保
        Destroy(reflect_obj[index]);                        //像のGameObjectを破壊

        origin_obj.RemoveAt(index);                         //映し元を削除
        reflect_obj.RemoveAt(index);                        //鏡側の像を削除
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

        reflect.GetComponent<MeshRenderer>().material = mirror_obj_material;        //マテリアル設定
        reflect.AddComponent<ReflectObject>();                                      //像のコンポーネント追加
        reflect.GetComponent<ReflectObject>().ReflectFrom(origin, dest_size);       //映し元とサイズ設定
        reflect.GetComponent<ReflectObject>().Reflect();                            //映す

        reflect_obj.Add(reflect);                           //管理リストに追加
    }
    /// <summary>
    /// 映すサイズ
    /// </summary>
    /// <param name="unresizable">大きさ変えられるか</param>
    /// <returns></returns>
    private Vector3 ReflectSize(bool unresizable)
    {
        if (unresizable)                                    //変わりたくないObjの場合
            return new Vector3(1, 1, 1);                    //サイズ変更なし
        return reflect_size;                                //プリセットのサイズ
    }

    /// <summary>
    /// 映すサイズ
    /// </summary>
    /// <returns></returns>
    public Vector3 ReflectSize()
    {
        return reflect_size;
    }

    /// <summary>
    /// 鏡の各辺の座標、サイズ
    /// </summary>
    /// <returns></returns>
    public Rect GetSide()
    {
        float xScale = GetComponent<BoxCollider>().size.x / 2 * transform.localScale.x;
        float yScale = GetComponent<BoxCollider>().size.y / 2 * transform.localScale.y;
        float x = transform.position.x - xScale;
        float y = transform.position.y - yScale;
        Rect rect = new Rect(x, y, xScale * 2, yScale * 2);
        return new Rect(x, y, xScale * 2, yScale * 2);
    }
}
