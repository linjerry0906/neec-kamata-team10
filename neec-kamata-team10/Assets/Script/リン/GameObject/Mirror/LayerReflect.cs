//------------------------------------------------------
// 作成日：2018.5.14
// 作成者：林 佳叡
// 内容：前景を鏡に反射されるようにするスクリプト
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerReflect : MonoBehaviour
{
    [SerializeField]
    private Material reflectMaterial;               //反射のマテリアル
    [SerializeField]
    private Transform parent;

	void Start ()
    {
        if (transform.name.Contains("Clone"))
            return;

        int backgroundLayer = 8;                    //レイヤーの番号
        Vector3 pos = transform.position;           //位置取得
        pos.z *= -1;                                //反射位置を設定
        GameObject reflect = Instantiate(this.gameObject, pos, Quaternion.identity, parent);    //オブジェクト作成
        reflect.layer = backgroundLayer;            //レイヤー設定
        GameObject child = reflect.transform.GetChild(0).gameObject;                            //子供
        child.layer = backgroundLayer;              //レイヤー設定
        child.GetComponent<TilemapRenderer>().material = reflectMaterial;                       //マテリアル設定
    }

}
