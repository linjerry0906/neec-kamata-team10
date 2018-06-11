using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MoveEnemy
{
    Animator anim;
    EnemyAnime enemyAnime;

    void Start()
    {
        DirectionInit();

        anim = GetComponent<Animator>();
        enemyAnime = GetComponent<EnemyAnime>();
    }

    void Update()
    {
        SetGroundEdge();                                    //地面端の設定
        HorizontalMove();                                   //行ったり来たり                                 
        enemyAnime.Animation(this);                         //反転アニメーション
    }



}


