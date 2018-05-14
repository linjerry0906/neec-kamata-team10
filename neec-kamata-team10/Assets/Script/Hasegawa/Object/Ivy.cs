using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ivy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ChangeCollider();
	}

    void ChangeCollider()
    {
        SizeEnum size = transform.GetComponent<ObjectSize>().GetSize();
        if (size == SizeEnum.Big_XY || size == SizeEnum.Big_Y)
        {
            transform.GetComponent<BoxCollider>().isTrigger = true;
        }
        else
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
