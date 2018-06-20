using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearBlockCollider : MonoBehaviour {

    private Player player;

	void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            //Debug.Log("enter");
            player = c.gameObject.GetComponent<Player>();
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            //Debug.Log("exit");
            player = null;
        }
    }

    void OnDisable()
    {
        if (player != null)
        {
            //Debug.Log("disable");
            player.SetIsJump(true);
        }
    }
}
