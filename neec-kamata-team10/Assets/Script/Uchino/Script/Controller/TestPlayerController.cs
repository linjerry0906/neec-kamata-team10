using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour {

    ICharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<PadController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(controller.HorizontalMove());
	}
}
