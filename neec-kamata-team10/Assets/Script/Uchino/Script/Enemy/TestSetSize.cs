using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetSize : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Enemy") { return; }

        ObjectSize size = other.GetComponent<ObjectSize>();
        size.SetSize(SizeEnum.Small_X);


    }
}
