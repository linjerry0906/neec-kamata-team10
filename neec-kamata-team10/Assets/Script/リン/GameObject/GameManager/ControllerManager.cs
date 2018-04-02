using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private KeboardController keyboard_controller;

    public ControllerManager()
    {
        Initialize();
    }

    private void Initialize()
    {
        keyboard_controller = new KeboardController();
    }

    public ICharacterController KeyboardController()
    {
        return keyboard_controller;
    }
}
