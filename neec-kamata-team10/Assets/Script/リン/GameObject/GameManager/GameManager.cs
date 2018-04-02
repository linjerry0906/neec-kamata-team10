using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private ControllerManager controller_manager;

    private void Awake()
    {
        controller_manager = new ControllerManager();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ICharacterController GetController(EController eController)
    {
        if(eController == EController.KEYBOARD)
            return controller_manager.Keyboard();

        return controller_manager.Pad();
    }
}
