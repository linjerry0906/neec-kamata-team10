using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundInfo : MonoBehaviour
{

    float leftEdgeX;    //地面の左端
    float rightEdgeX;   //地面の右端

    /// <summary>
    /// 地面端のX座標をセット
    /// </summary>
    /// <param name="transform"></param>
    public void SetEdgeOfTheGround(Transform transform)
    {
        Vector3 groundPos = transform.position;                 //地面の位置を一時保存

        leftEdgeX  = groundPos.x - transform.localScale.x / 2;  //左端を求める
        rightEdgeX = groundPos.x + transform.localScale.x / 2;  //右端を求める
    }

    /// <summary>
    /// 左端を取得
    /// </summary>
    public float LeftEdgeX
    {
        get { return leftEdgeX; }
    }

    /// <summary>
    /// 右端を取得
    /// </summary>
    public float RightEdgeX
    {
        get { return rightEdgeX; }
    }

}
