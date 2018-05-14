using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    [SerializeField]
    private float angle = 45;           //角度

    [SerializeField]
    private float addPower = 10;        //加える大きさ

	// Use this for initialization
	void Start ()
    {
        Vector3 force = addPower * AngleToVector3();            //向きに力を加えて飛ぶベクトルを計算

        Rigidbody rd = gameObject.GetComponent<Rigidbody>();    //RigidBodyの取得

        rd.AddForce(-force,ForceMode.Impulse);                   //一気にぶっ飛ぶ（重力と空気抵抗で減衰しながら）
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// 角度からベクトルへ
    /// </summary>
    /// <returns></returns>
    private Vector3 AngleToVector3()
    {
        Vector3 velocity = Vector3.zero;

        velocity.x = Mathf.Cos(angle * Mathf.PI / 180);
        velocity.y = Mathf.Sign(angle * Mathf.PI / 180);
        velocity.z = 0;

        return velocity;
    }
}
