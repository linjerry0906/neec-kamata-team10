//------------------------------------------------------
// 作成日：2018.3.28
// 作成者：林 佳叡
// 内容：生成された像のクラス
//------------------------------------------------------
using UnityEngine;

public class ReflectObject : MonoBehaviour
{
    private SizeEnum size;
    private GameObject originObj;      //映し元
    private Vector3 reflectSize;       //指定サイズ
    private Vector3 sizeRate;

    /// <summary>
    /// どのObjを反映するか
    /// </summary>
    /// <param name="originObj">映し元</param>
    /// <param name="size">拡大・縮小のサイズ</param>
    public void ReflectFrom(GameObject originObj, Vector3 size, SizeEnum sizeEnum)
    {
        gameObject.layer = 8;
        this.originObj = originObj;
        this.size = sizeEnum;
        reflectSize = originObj.transform.localScale;      //映し元のサイズ指定
        ObjectSize objSize = originObj.GetComponent<ObjectSize>();
        if (objSize)
            reflectSize = objSize.DefaultSize();
        reflectSize.Scale(size);                           //拡大縮小したサイズ
        sizeRate = size;

        InitMeshRenderer();
    }

    /// <summary>
    /// メッシュレンダラーの初期化
    /// </summary>
    private void InitMeshRenderer()
    {
        if (originObj.tag != "appear_block")
            return;

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Material originMaterial = originObj.GetComponent<MeshRenderer>().material;
        mesh.material.SetTexture("_DesolveTex", originMaterial.GetTexture("_DesolveTex"));
        mesh.material.SetColor("_ApearColor", originMaterial.GetColor("_ApearColor"));
        mesh.material.SetColor("_ApearColor2", originMaterial.GetColor("_ApearColor2"));
        mesh.material.SetFloat("_ApearSize", originMaterial.GetFloat("_ApearSize"));
        mesh.material.SetFloat("_Disappear", 1.0f);
    }

    /// <summary>
    /// 映した像の形を反映する
    /// </summary>
    public void Reflect()
    {
        ReflectToOrigin();
        ReflectPosition();      //位置を反映する
        ReflectRotation();      //回転を反映する
        ReflectScale();         //大きさを反映する
        ReflectSprite();
        ReflectMesh();
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
    /// 映し元を反映
    /// </summary>
    private void ReflectToOrigin()
    {
        ObjectSize objSize = originObj.GetComponent<ObjectSize>();
        if (objSize)
        {
            objSize.SetSize(size);
            objSize.SetReflectSize(sizeRate);
        }
    }

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
    }

    private void ReflectSprite()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (!sprite)
            return;
        sprite.sprite = originObj.GetComponent<SpriteRenderer>().sprite;
    }

    private void ReflectMesh()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (!mesh)
            return;

        Material originMaterial = originObj.GetComponent<MeshRenderer>().material;
        mesh.material.SetColor("_EmissionColor", originMaterial.GetColor("_EmissionColor"));
        mesh.material.SetColor("_Color", originMaterial.GetColor("_Color"));
    }
}
