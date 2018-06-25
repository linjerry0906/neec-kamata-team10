using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<AliveFlag>().Dead();
        else if(other.CompareTag("Enemy") || other.CompareTag("stage_block"))
            Destroy(other.gameObject);
    }
}
