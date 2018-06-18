using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearBlockCollider : MonoBehaviour {

    private Player player;

	void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            player = c.gameObject.GetComponent<Player>();
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            player = null;
        }
    }

    void OnDisable()
    {
        if (player != null)
        {
            player.SetIsJump(true);
        }
    }
}
