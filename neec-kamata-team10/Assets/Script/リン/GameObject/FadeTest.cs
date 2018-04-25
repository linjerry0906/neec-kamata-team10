using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTest : MonoBehaviour {

    float time = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        time += 1.0f;

        if (time >= 360)
            GetComponent<NextScene>().Change();
	}
}
