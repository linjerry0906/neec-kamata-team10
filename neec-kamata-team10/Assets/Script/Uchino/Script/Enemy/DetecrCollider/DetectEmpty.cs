using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEmpty : MonoBehaviour
{
    GroundInfo groundInfo;                                                  //地面端の位置クラス

    private void Start()
    {
        groundInfo = GetComponentInParent<GroundInfo>();                    //実体を取得
    }


    bool isCollison = true;
    private void OnTriggerStay(Collider other)
    {
        //地面に触れている間は常にtrue
        isCollison = true;
    }

    /// <summary>
    /// 地面の接触判定用メソッド
    /// </summary>
    public void MyUpdate()
    {
        isCollison = false; //常にfalse
    }

    /// <summary>
    /// 地面に接触しているか否か
    /// </summary>
    /// <returns></returns>
    public bool IsCollison()
    {
        return isCollison;
    }

    /// <summary>
    /// 地面端の位置を保存
    /// </summary>
    /// <param name="direction"></param>
    public void SetGroundEdge(Direction direction)
    {
        groundInfo.SetEdgeOfTheGround(direction, transform.position.x);               //地面端の位置を保存
    }


}
