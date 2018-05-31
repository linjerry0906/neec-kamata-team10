using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splinter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //他のコライダと接触した時
    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Player")
        {
            col.gameObject.GetComponent<AliveFlag>().Dead();
        }
    }
}
