using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEmpty : MonoBehaviour
{
    bool isFirstOnTrigger = false; //衝突したか否か

    /// <summary>
    /// 地面の端を取得
    /// </summary>
    /// <param name="ground"></param>
    private void OnTriggerEnter(Collider ground)
    {
        if (isFirstOnTrigger){ return; }                             //最初の一回だけ実行

        isFirstOnTrigger = true;                                    //trueにして二度実行させない

        GroundInfo groundInfo = GetComponentInParent<GroundInfo>(); 
        groundInfo.SetEdgeOfTheGround(ground.transform);            //GroundInfoに地面の端を求めてもらう。
    }


    /// <summary>
    /// 地面の端まで来たら方向を反転させる。(落ちないように)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "mirror")
            return;

        Debug.Log(other.name);
        GetComponentInParent<ChaseEnemy>().ReverseDirection();      //移動方向を反転させる。
    }

    private void PositionCorrection()
    {
        if(transform.position.y == -1) { return; }

        transform.position =
            new Vector3(transform.position.x, -1, transform.position.z);
        Debug.Log(transform.position.y);
    }

    private void ScaleOrSizeCorrection()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    
}

