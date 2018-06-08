using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlockScript : MonoBehaviour {
    
    [SerializeField]
    GameObject gameObject;
    [SerializeField]
    int breakMass;
    [SerializeField]
    float breakTime;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }

    //他のコライダと接触した時
    void OnCollisionEnter(Collision col)
    {
        if (col.rigidbody.mass >= breakMass)
        {
            breakTime--;
            if (breakTime <= 0)  Destroy(gameObject);
        }
            
    }

    //他のコライダと接触している時
    void OnCollisionStay(Collision col)
    {
    }

    //他のコライダと離れた時
    void OnCollisionExit(Collision col)
    {
    }
    
}
