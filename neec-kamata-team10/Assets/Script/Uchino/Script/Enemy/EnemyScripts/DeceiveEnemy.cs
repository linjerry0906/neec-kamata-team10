using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeceiveEnemy : MonoBehaviour {

    Animator anim;
    DetectRange detectRange;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        detectRange = GetComponentInChildren<DetectRange>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("IsMove", detectRange.IsMove());

	}
}
