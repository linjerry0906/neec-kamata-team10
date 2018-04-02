using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController 
{
    Vector3 HorizontalMove();
    bool    Jump();
}
