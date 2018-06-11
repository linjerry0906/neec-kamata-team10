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
    float time;
    bool trigger;

    // Use this for initialization
    void Start () {
        time = breakTime;
        trigger = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (trigger)
        {
            time--;
            if (time <= 0) Destroy(gameObject);
        }
    }
    

    //他のコライダと接触した時
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            trigger = true;
        }
    }
    
}
