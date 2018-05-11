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

    /// 地面の端まで来たら方向を反転させる。(落ちないように)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "mirror")
            return;

        //Debug.Log(name);
        //GetComponentInParent<MoveEnemy>().ReverseDirection();                   //移動方向を反転させる。
        //Debug.Log(GetComponentInParent<MoveEnemy>().Direction);
    }

    bool isCollison = true;
    private void OnTriggerStay(Collider other)
    {
        isCollison = true;
    }

    public void MyUpdate()
    {
        isCollison = false;
    }

    /// <summary>
    /// 当たっているか否か
    /// </summary>
    /// <returns></returns>
    public bool IsCollison()
    {
        return isCollison;
    }


    public void SetGroundEdge(Direction direction)
    {

        groundInfo.SetEdgeOfTheGround(direction, transform.position.x);               //地面端の位置を保存
            
    }


}
