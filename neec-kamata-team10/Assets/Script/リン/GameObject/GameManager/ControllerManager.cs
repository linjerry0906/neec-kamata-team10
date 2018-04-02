using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private KeboardController keyboard_controller;
    private KeboardController pad_controller;

    public ControllerManager()
    {
        Initialize();
    }

    private void Initialize()
    {
        keyboard_controller = new KeboardController();
        pad_controller = new KeboardController();
    }

    public ICharacterController Keyboard()
    {
        return keyboard_controller;
    }

    public ICharacterController Pad()
    {
        return pad_controller;
    }
}
