using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MoveEnemy
{
    private Animator anim;

    void Start()
    {
        DirectionInit();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SetGroundEdge();        //地面端の設定
        HorizontalMove();       //行ったり来たり
        anim.SetInteger("direction", (int)Direction);
    }

}


