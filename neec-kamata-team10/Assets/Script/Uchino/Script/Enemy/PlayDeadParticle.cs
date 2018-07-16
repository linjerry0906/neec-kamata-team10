using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDeadParticle : MonoBehaviour {

    EnemyDead enemyDead;

	// Use this for initialization
	void Start () {
        enemyDead = GetComponentInParent<EnemyDead>();
	}
	
	//敵死亡時のパーティクル再生
	void Update () {
		if(enemyDead.IsDead())
        {
            GetComponent<ParticleSystem>().Play();
        }
	}
}
