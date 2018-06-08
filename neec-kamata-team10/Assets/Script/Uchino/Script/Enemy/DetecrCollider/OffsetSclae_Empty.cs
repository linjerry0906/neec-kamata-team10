﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetSclae_Empty : MonoBehaviour {

    private Vector3 defaultScale;
    // Use this for initialization
    void Start () {
        defaultScale = transform.lossyScale;
    }

    private void Update()
    {

        Vector3 lossScale = transform.lossyScale;
        Vector3 localScale = transform.localScale;

        transform.localScale = new Vector3(
                localScale.x / lossScale.x * defaultScale.x,
                localScale.y / lossScale.y * defaultScale.y,
                localScale.z / lossScale.z * defaultScale.z
                );
    }
}