﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPos;
    private int num = 0;

    void Start()
    {
        respawnPos = transform.parent.position;
    }
     
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Respawn"))
        {
            int spawnNum = c.gameObject.GetComponent<RespawnPoint>().GetNumber();

            if (spawnNum < num) return;
            num = spawnNum;
            respawnPos = c.transform.position;
            respawnPos.y += 0.2f;
        }
    }

    public Vector3 GetRespawnPosition()
    {
        return respawnPos;
    }
}
