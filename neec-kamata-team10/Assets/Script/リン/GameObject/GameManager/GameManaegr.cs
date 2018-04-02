using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManaegr : MonoBehaviour {

    private ControllerManager controller_manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ICharacterController GetController()
    {
        return controller_manager.KeyboardController();
    }
}
