using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<AliveFlag>().Dead();
        else
            Destroy(other.gameObject);
    }
}
