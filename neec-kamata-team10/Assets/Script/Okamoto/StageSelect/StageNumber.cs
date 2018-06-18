﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNumber : MonoBehaviour {

    private int stage;
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        stage = transform.parent.GetComponent<StageSelectScript>().StageNumber();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Stage" + stage;
	}
}