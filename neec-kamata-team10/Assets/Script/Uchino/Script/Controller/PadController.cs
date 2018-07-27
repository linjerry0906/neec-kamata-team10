using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : ICharacterController
{

    /// <summary>
    /// 水平移動
    /// </summary>
    /// <returns></returns>
    public Vector3 HorizontalMove()
    {
        Vector3 velocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") <= -0.05f)
        {
            velocity = new Vector3(-1, 0, 0);
        }
        if (Input.GetAxisRaw("Horizontal") >= 0.05f)
        {
            velocity = new Vector3(1, 0, 0);
        }

        return velocity;
    }

    /// <summary>
    /// 水平移動
    /// </summary>
    /// <returns></returns>
    public Vector3 VerticalMove()
    {
        Vector3 velocity = Vector3.zero;

        if (Input.GetAxisRaw("Vertical") <= -0.05f)
        {
            velocity = new Vector3(0, -1, 0);
        }
        if (Input.GetAxisRaw("Vertical") >= 0.05f)
        {
            velocity = new Vector3(0, 1, 0);
        }

        return velocity;
    }


    /// <summary>
    /// 右に切り替えるか
    /// </summary>
    /// <returns></returns>
    public bool SwitchToTheRight()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton5)) //R
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
        if (Input.GetKeyDown(KeyCode.JoystickButton4)) //L
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
        if (Input.GetKeyDown(KeyCode.JoystickButton0)) //A
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
        if (Input.GetKeyDown(KeyCode.JoystickButton2)) //X
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
        if (Input.GetKeyUp(KeyCode.JoystickButton2)) //X
        {
            return true;
        }

        return false;
    }

    int pressFlame = 0;
    /// <summary>
    /// カウント数を数える
    /// </summary>
    public void CountFlameUpdate()
    {
        if(Input.GetKey(KeyCode.JoystickButton2))
        {
            pressFlame++;
            return;
        }

        CountFlameInit();
    }

    /// <summary>
    /// フレーム数を初期化する。
    /// </summary>
    public void CountFlameInit()
    {
        pressFlame = 0;
    }

    /// <summary>
    /// フレーム数を取得する。
    /// </summary>
    /// <returns></returns>
    public int GetFlameCount()
    {
        return pressFlame;
    }

    bool isFade = false;
    public bool IsFade()
    {
        return isFade;
    }

    public void SetFadeFlag(bool isFade)
    {
        this.isFade = isFade;
    }

    /// <summary>
    /// 選択を上に移動
    /// </summary>
    /// <returns></returns>
    public bool MoveSelectionUp()
    {
        return IsKeyDown(isUp,previousUp);
    }

    /// <summary>
    /// 選択を下に移動
    /// </summary>
    /// <returns></returns>
    public bool MoveSelectionDown()
    {
        return IsKeyDown(isDown, previousDown);
    }

    /// <summary>
    /// 選択を左に移動
    /// </summary>
    /// <returns></returns>
    public bool MoveSelectionLeft()
    {
        return IsKeyDown(isLeft, previousLeft);
    }

    /// <summary>
    /// 選択を右に移動
    /// </summary>
    /// <returns></returns>
    public bool MoveSelectionRight()
    {
        return IsKeyDown(isRight, previousRight);
    }

    public void Update()
    {
        UpdateKey();
    }
    
    //現在押されたか
    bool isRight = false;
    bool isLeft = false;
    bool isUp = false;
    bool isDown = false;

    //前回押されたか
    bool previousRight = false;
    bool previousLeft = false;
    bool previousDown = false;
    bool previousUp = false;

    //軸(ジョイスティック)
    string vertical = "Vertical";
    string horizontal = "Horizontal";

    const float feelingMovementNum = 0.75f; 

    private void UpdateKey()
    {
        previousRight = isRight;
        previousLeft  = isLeft;
        previousDown  = isDown;
        previousUp    = isUp;

        isRight = IsKeyDown(horizontal, 1);
        isLeft  = IsKeyDown(horizontal,-1);
        isUp    = IsKeyDown(vertical,   1);
        isDown  = IsKeyDown(vertical,  -1);
    }

    private bool IsKeyDown(string axisName, float direction)
    {
        //アナログスティックの軸名（UnityのInputManagerで設定）
        string analogSticName = axisName + 2;

        //＋軸なら
        if(direction >= 0)
        {
            return IsPlusInput(axisName,analogSticName);
        }
        //-軸なら
        if (direction <= 0)
        {
            return IsMinusInput(axisName,analogSticName);
        }

        return false;
    }

    /// <summary>
    /// プラス軸の入力判定
    /// </summary>
    /// <param name="joyAxisName"></param>
    /// <param name="analogAxisName"></param>
    /// <returns></returns>
    bool IsPlusInput(string joyAxisName,string analogAxisName)
    {
        if (Input.GetAxisRaw(joyAxisName) >= feelingMovementNum) return true;
        if (Input.GetAxis(analogAxisName) == 1)                  return true;

        return false;
    }

    /// <summary>
    /// マイナス軸の入力判定
    /// </summary>
    /// <param name="joyAxisName"></param>
    /// <param name="analogAxisName"></param>
    /// <returns></returns>
    bool IsMinusInput(string axisName, string analogAxisName)
    {
        if (Input.GetAxisRaw(axisName) <= -feelingMovementNum) return true;
        if (Input.GetAxis(analogAxisName) == -1)               return true;

        return false;
    }

    /// <summary>
    /// 前回のキーが押されてなくて、今回のキーが押されていたら。
    /// </summary>
    /// <param name="isDirKey"></param>
    /// <param name="previousDirKey"></param>
    /// <returns></returns>
    private bool IsKeyDown(bool isDirKey ,bool previousDirKey)
    {
        return isDirKey && !previousDirKey;
    }

	/// <summary>
	/// Pauseボタン
	/// </summary>
	public bool Pause()
	{
		return Input.GetKeyDown(KeyCode.JoystickButton7);		
	}

    public bool IsConnectedPad()
    {
        string[] ar = Input.GetJoystickNames();
        return ar.Length > 0; //コントローラは存在するか?
    }

    /// <summary>
    /// リスポーン（即時やりなおし）
    /// </summary>
    /// <returns></returns>
    public bool Respawn()
    {
        return Input.GetKeyDown(KeyCode.Joystick1Button9);  //ジョイスティックの窪み
    }

    /// <summary>
    /// ゲームを終了
    /// </summary>
    /// <returns></returns>
    public bool GameEnd()
    {
        if (!Input.GetKey(KeyCode.JoystickButton6)) return false;
        if (!Input.GetKey(KeyCode.JoystickButton7)) return false;

        return true;
    }
}
