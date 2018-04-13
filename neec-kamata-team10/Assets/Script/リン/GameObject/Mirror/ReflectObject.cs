//------------------------------------------------------
// 作成日：2018.3.28
// 作成者：林 佳叡
// 内容：生成された像のクラス
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectObject : MonoBehaviour {

    private GameObject origin_obj;      //映し元
    private Vector3 reflect_size;       //指定サイズ

    /*別仕様
    private GameObject parent_obj;
    private Vector3 size;
    */

    /// <summary>
    /// どのObjを反映するか
    /// </summary>
    /// <param name="origin_obj">映し元</param>
    /// <param name="size">拡大・縮小のサイズ</param>
    public void ReflectFrom(GameObject origin_obj, Vector3 size)
    {
        /*別仕様
        parent_obj = new GameObject(origin_obj.name + "_reflect");
        transform.position = parent_obj.transform.position;
        transform.parent = parent_obj.transform;
        this.size = size;
        */

        this.origin_obj = origin_obj;
        reflect_size = origin_obj.transform.localScale;     //映し元のサイズ指定
        reflect_size.Scale(size);                           //拡大縮小したサイズ
    }

    /// <summary>
    /// 映した像の形を反映する
    /// </summary>
    public void Reflect()
    {
        ReflectPosition();      //位置を反映する
        ReflectRotation();      //回転を反映する
        ReflectScale();         //大きさを反映する
    }

    /// <summary>
    /// 位置を反映する
    /// </summary>
    private void ReflectPosition()
    {
        Vector3 reflect_pos = origin_obj.transform.position;    //位置記録
        reflect_pos.z *= -1;                                    //反対側にする（2D横スクロールなので、Z = 0を反射面にする）
        transform.position = reflect_pos;                       //像の位置を設定
    }
    /// <summary>
    /// 回転を反映する
    /// </summary>
    private void ReflectRotation()
    {
        Quaternion reflect_rotation = origin_obj.transform.rotation;    //回転状態記録
        reflect_rotation.y *= -1;                                       //Z軸以外の回転を反対側にする
        reflect_rotation.x *= -1;                                       //Z軸以外の回転を反対側にする
        transform.eulerAngles = reflect_rotation.eulerAngles;           //像の回転を設定
    }
    /// <summary>
    /// 大きさを反映する
    /// </summary>
    private void ReflectScale()
    {
        Vector3 reflect_scale = reflect_size;                   //サイズ記録
        reflect_scale.z *= -1;                                  //サイズのZ軸を反対側にする
        transform.localScale = reflect_scale;                   //サイズ設定

        /*別仕様
        parent_obj.transform.localScale = size;
        */
    }
}
