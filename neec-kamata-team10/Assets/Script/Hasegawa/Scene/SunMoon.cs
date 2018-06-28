using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void Change(bool isSun)
    {
        if (isSun)
        {
            transform.GetChild(1).GetChild(3).gameObject.SetActive(true);
            transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(1).GetChild(3).gameObject.SetActive(false);
            transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
        }
    }
}
