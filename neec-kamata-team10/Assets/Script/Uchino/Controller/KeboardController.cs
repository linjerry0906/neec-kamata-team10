using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeboardController :ICharacterController
{
    private Vector2 velocity;

    public Vector2 Velocity
    {
        get { return velocity; }
    }


    public Vector3 HorizontalMove()
    {
        Vector3 velocity = Vector3.zero;

        if(Input.GetKey(KeyCode.RightArrow))
        {
            velocity = new Vector3(1, 0, 0);
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            velocity = new Vector3(-1, 0, 0);
        }

        return velocity;
    }

    public bool Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        return false;
    }


}
