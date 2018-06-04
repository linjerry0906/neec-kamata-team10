//------------------------------------------------------
// 作成日：2018.3.28
// 作成者：林 佳叡
// 内容：生成された像のクラス
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectObject : MonoBehaviour
{

    private SizeEnum size;
    private GameObject originObj;      //映し元
    private Vector3 reflectSize;       //指定サイズ
    //private GameObject mirror;
    //private Vector3 relativePos = Vector3.zero;
    //private bool onHand = false;

    /*別仕様
    private GameObject parent_obj;
    private Vector3 size;
    */

    /// <summary>
    /// どのObjを反映するか
    /// </summary>
    /// <param name="originObj">映し元</param>
    /// <param name="size">拡大・縮小のサイズ</param>
    public void ReflectFrom(GameObject originObj, Vector3 size, SizeEnum sizeEnum)
    {
        /*別仕様
        parent_obj = new GameObject(origin_obj.name + "_reflect");
        transform.position = parent_obj.transform.position;
        transform.parent = parent_obj.transform;
        this.size = size;
        */
        gameObject.layer = 8;
        this.originObj = originObj;
        this.size = sizeEnum;
        reflectSize = originObj.transform.localScale;      //映し元のサイズ指定
        ObjectSize objSize = originObj.GetComponent<ObjectSize>();
        if (objSize)
            reflectSize = objSize.DefaultSize();
        reflectSize.Scale(size);                           //拡大縮小したサイズ
    }

    /// <summary>
    /// 映した像の形を反映する
    /// </summary>
    public void Reflect()
    {
        //if (isHand)
        //    UpdateOnHand();

        ReflectToOrigin();
        ReflectPosition();      //位置を反映する
        ReflectRotation();      //回転を反映する
        ReflectScale();         //大きさを反映する
        ReflectSprite();
    }

    /// <summary>
    /// 映し元またあるかをチェック
    /// </summary>
    /// <returns></returns>
    public bool CheckInstance()
    {
        return originObj != null;
    }

    /// <summary>
    /// 手に持っている鏡と一緒に移動
    /// </summary>
    //private void UpdateOnHand()
    //{
    //    if (size != SizeEnum.Normal)
    //        transform.position = mirror.transform.position + relativePos;
    //}

    /// <summary>
    /// 映し元を反映
    /// </summary>
    private void ReflectToOrigin()
    {
        if (originObj.tag.Equals("Unresizable"))
            return;
        ObjectSize objSize = originObj.GetComponent<ObjectSize>();
        if (objSize)
        {
            objSize.SetSize(size);
            //SetOriginActive(isHand);
        }

        //if (!isHand)                     //鏡が手に持っていない場合
        //    return;

        //Vector3 originPos = originObj.transform.position;
        //originPos.x = transform.position.x;
        //originPos.y = transform.position.y;
        //originObj.transform.position = originPos;
    }

    /// <summary>
    /// 描画するかどうかを設定
    /// </summary>
    /// <param name="isHand"></param>
    //private void SetOriginActive(bool isHand)
    //{
    //    if (isHand == onHand)
    //        return;

    //    onHand = isHand;
    //    FlipComponent(ref originObj, !isHand);
    //}

    /// <summary>
    /// Componentをオン・オフにする
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="flag"></param>
    //private void FlipComponent(ref GameObject obj, bool flag)
    //{
    //    Renderer renderer = obj.GetComponent<Renderer>();               //描画
    //    if (renderer)
    //        renderer.enabled = flag;
    //    Collider collider = obj.GetComponent<Collider>();               //コライダー
    //    if (collider)
    //        collider.enabled = flag;
    //    Rigidbody rigid = obj.GetComponent<Rigidbody>();                //RigidBody
    //    if (rigid)
    //        rigid.velocity = Vector3.zero;

    //    foreach (MonoBehaviour m in obj.GetComponents<MonoBehaviour>()) //コンポーネント
    //    {
    //        m.enabled = !onHand;
    //    }

    //    for (int i = 0; i < obj.transform.childCount; i++)              //子供
    //    {
    //        GameObject child = obj.transform.GetChild(i).gameObject;
    //        FlipComponent(ref child, flag);
    //    }
    //}

    /// <summary>
    /// 位置を反映する
    /// </summary>
    private void ReflectPosition()
    {
        Vector3 reflect_pos = originObj.transform.position;     //位置記録
        reflect_pos.z *= -1;                                    //反対側にする（2D横スクロールなので、Z = 0を反射面にする）
        transform.position = reflect_pos;                       //像の位置を設定
    }
    /// <summary>
    /// 回転を反映する
    /// </summary>
    private void ReflectRotation()
    {
        Quaternion reflect_rotation = originObj.transform.rotation;     //回転状態記録
        reflect_rotation.y *= -1;                                       //Z軸以外の回転を反対側にする
        reflect_rotation.x *= -1;                                       //Z軸以外の回転を反対側にする
        transform.eulerAngles = reflect_rotation.eulerAngles;           //像の回転を設定
    }
    /// <summary>
    /// 大きさを反映する
    /// </summary>
    private void ReflectScale()
    {
        Vector3 reflect_scale = reflectSize;                    //サイズ記録
        reflect_scale.z *= -1;                                  //サイズのZ軸を反対側にする
        float flip = originObj.transform.localScale.x;
        if (Mathf.Sign(flip) != Mathf.Sign(reflect_scale.x))
            reflect_scale.x *= -1;
        transform.localScale = reflect_scale;                   //サイズ設定

        /*別仕様
        parent_obj.transform.localScale = size;
        */
    }

    private void ReflectSprite()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (!sprite)
            return;
        sprite.sprite = originObj.GetComponent<SpriteRenderer>().sprite;
    }

    //public void SetMirror(GameObject mirror)
    //{
    //    this.mirror = mirror;
    //    relativePos = transform.position - mirror.transform.position;
    //}

    //public void ResetPos()
    //{
    //    UpdateOnHand();

    //    Vector3 originPos = originObj.transform.position;
    //    originPos.x = transform.position.x;
    //    originPos.y = transform.position.y;
    //    originObj.transform.position = originPos;
    //}
}
