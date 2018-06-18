using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MoveEnemy
{
    Animator anim;

    void Start()
    {
        DirectionInit();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SetGroundEdge();                                    //地面端の設定
        HorizontalMove();                                   //行ったり来たり

        FlipAnimation();                                    //反転アニメーション
    }





}


