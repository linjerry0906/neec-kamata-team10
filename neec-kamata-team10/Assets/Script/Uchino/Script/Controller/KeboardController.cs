using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeboardController : ICharacterController
{
    /// <summary>
    /// 水平移動
    /// </summary>
    /// <returns></returns>
    public Vector3 HorizontalMove()
    {
        Vector3 velocity = Vector3.zero;

        if(Input.GetKey((KeyCode.LeftArrow)))
        {
            velocity = new Vector3(-1, 0, 0);
        }
        if(Input.GetKey((KeyCode.RightArrow)))
        {
            velocity = new Vector3(1, 0, 0);
        }

        return velocity;
    }

    /// <summary>
    /// 垂直移動
    /// </summary>
    /// <returns></returns>
    public Vector3 VerticalMove()
    {
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            velocity = new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity = new Vector3(0, -1, 0);
        }

        return velocity;

    }

    /// <summary>
    /// 右に切り替えるか
    /// </summary>
    /// <returns></returns>
    public bool SwitchToTheRight()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
        if (Input.GetKeyDown((KeyCode.Q)))
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
        if(Input.GetKeyDown(KeyCode.W))
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

    /// <summary>
    /// 鏡を投げる（長押しした分だけ距離を延ばす）
    /// </summary>
    /// <returns></returns>
    public bool ThrowMirror()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            return true;
        }

        return false;
    }

    
    int pressFlame = 0;
    /// <summary>
    /// フレーム数を数える。
    /// </summary>
    public void CountFlameUpdate()
    {
        if(Input.GetKey(KeyCode.UpArrow))   //↑キーを押していたら数える
        {
            pressFlame++;
            return;
        }

        CountFlameInit();                   //押していない間は数えない
    }
    
    /// <summary>
    /// フレーム数を取得する。
    /// </summary>
    /// <returns></returns>
    public int GetFlameCount()
    {
        return pressFlame;
    }


    /// <summary>
    /// フレーム数を初期化する。
    /// </summary>
    public void CountFlameInit()
    {
        pressFlame = 0;
    }

    bool isFade = false;

    /// <summary>
    /// シーンフェードしたか
    /// </summary>
    /// <returns></returns>
    public bool IsFade()
    {
        return isFade;
    }

    /// <summary>
    /// フェードしたか否か
    /// </summary>
    /// <param name="isFade"></param>
    public void SetFadeFlag(bool isFade)
    {
        this.isFade = isFade;
    }
    
}
