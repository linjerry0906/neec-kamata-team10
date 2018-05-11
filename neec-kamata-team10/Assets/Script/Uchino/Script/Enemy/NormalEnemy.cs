using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MoveEnemy
{
    void Start()
    {
        DirectionInit();
    }

    void Update()
    {
        SetGroundEdge();        //地面端の設定
        HorizontalMove();       //行ったり来たり
    }




}


