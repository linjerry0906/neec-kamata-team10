using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeboardController :ICharacterController
{
    private Vector2 velocity;

    public Vector2 Velocity
    {
        get { return velocity; }
    }

    /// <summary>
    /// 水平移動
    /// </summary>
    /// <returns></returns>
    public Vector3 HorizontalMove()
    {
        Vector3 velocity = Vector3.zero;

        if(Input.GetKey(KeyCode.A))
        {
            velocity = new Vector3(1, 0, 0);
        }
        if(Input.GetKey(KeyCode.D))
        {
            velocity = new Vector3(-1, 0, 0);
        }

        return velocity;
    }

    /// <summary>
    /// 右に切り替えるか
    /// </summary>
    /// <returns></returns>
    public bool SwitchToTheRight()
    {
        if(Input.GetKeyDown((KeyCode.RightArrow)))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 左に切り替えるか
    /// </summary>
    /// <returns></returns>
    public bool SwitchToTheLeft()
    {
        if (Input.GetKeyDown((KeyCode.LeftArrow)))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 鏡の操作　(新規設置:鏡を持つ:鏡を置く)
    /// </summary>
    /// <returns></returns>
    public bool OperateTheMirror()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    /// <returns></returns>
    public bool Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        return false;
    }


}
