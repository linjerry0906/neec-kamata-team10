using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAliveFlag : MonoBehaviour
{
    bool isDead = false;

    public void Dead()
    {
        isDead = true;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
