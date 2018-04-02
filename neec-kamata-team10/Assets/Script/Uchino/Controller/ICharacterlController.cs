using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController 
{
    Vector3 HorizontalMove(); //水平移動
    bool Jump();              //ジャンプ
    bool SwitchToTheRight();  //鏡を右に切り替える
    bool SwitchToTheLeft();   //鏡を左に切り替える
    bool OperateTheMirror();　//鏡の操作

}
