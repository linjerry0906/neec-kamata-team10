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
        HorizontalMove();
    }


}


