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
        if (other.transform.parent.name.Contains("Stage")) { return; }

        Debug.Log(other.name);

        GetComponentInParent<MoveEnemy>().ReverseDirection();                   //移動方向を反転させる。

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == null) { Debug.Log("null"); }
    }

    private void SetGroundEdge()
    {
        if (groundInfo.IsSetEdge()) { return; }                                 //地面端の位置が保存されていたら実行しない
        Debug.Log("Set");
        Direction dir = GetComponentInParent<MoveEnemy>().Direction;            //ChaseEnemyの向きを取得
        groundInfo.SetEdgeOfTheGround(dir, transform.position.x);               //地面端の位置を保存
            
    }

}
