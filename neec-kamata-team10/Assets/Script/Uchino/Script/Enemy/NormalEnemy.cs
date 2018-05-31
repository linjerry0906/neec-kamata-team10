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
        SetGroundEdge();                                    //地面端の設定
        HorizontalMove();                                   //行ったり来たり

        Animation();
        //anim.SetInteger("direction", (int)Direction);       //向きに合わせてdirectionも変動
        
    }

    void Animation()
    {
        Vector3 scale = transform.localScale;

        transform.localScale = new Vector3(scale.x * -(int)Direction,scale.y,scale.z);
    }



}


