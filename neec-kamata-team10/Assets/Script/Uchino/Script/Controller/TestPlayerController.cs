using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour {

    ICharacterController controller;

	// Use this for initialization
	void Start () {
        controller = new KeboardController();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
}
