using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundInfo : MonoBehaviour
{

    float leftEdgeX  =0;    //地面の左端
    float rightEdgeX =0;    //地面の右端

    /// <summary>
    /// セットされているか(各方向)
    /// </summary>
    /// <returns></returns>
    public bool IsSetEdge(Direction direction)
    {

        if (direction == Direction.LEFT)  return IsSetLeft();
        if (direction == Direction.RIGHT) return IsSetRight();

        return false; //到達しない
    }

    /// <summary>
    /// セットされているか
    /// </summary>
    /// <returns></returns>
    public bool IsAllSet()
    {
        if(rightEdgeX == 0) { return false; }
        if(leftEdgeX  == 0) { return false; }

        return true;
    }

    /// <summary>
    /// 右端がセットされているか
    /// </summary>
    /// <returns></returns>
    public bool IsSetRight()
    {
        if (rightEdgeX == 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 左端がセットされているか
    /// </summary>
    /// <returns></returns>
    public bool IsSetLeft()
    {
        if(leftEdgeX == 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 地面端のX座標をセット
    /// </summary>
    /// <param name="transform"></param>
    public void SetEdgeOfTheGround(Direction direction,float edge)
    {
        if (IsSetEdge(direction)) return;

        if (direction == Direction.LEFT)  { leftEdgeX  = edge; Debug.Log(LeftEdgeX);  }
        if (direction == Direction.RIGHT) { rightEdgeX = edge; Debug.Log(rightEdgeX); }
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
