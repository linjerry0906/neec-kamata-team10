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
        Vector3 forceDirection = AngleToVector3();              //角度をベクトルに

        forceDirection.x *= (int)direction;                     //発射する方向をエネミーが向いている方向にする

        Vector3 force = addPower * forceDirection;              //向きに力を加えて飛ぶベクトルを計算

        Rigidbody rd = gameObject.GetComponent<Rigidbody>();    //RigidBodyの取得

        rd.AddForce(force,ForceMode.Impulse);                   //一気にぶっ飛ぶ（重力と空気抵抗で減衰しながら）

    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 velocity = GetComponent<Rigidbody>().velocity;

        if (velocity.y == 0){ Destroy(gameObject);}             //Yベクトルの大きさが0になれば消去
    }

    private Direction direction = Direction.LEFT;               //ThrowEnemyの向き
    /// <summary>
    /// 方向がセットされる
    /// </summary>
    /// <param name="direction"></param>
    public void SetDircetion(Direction direction)
    {
        this.direction = direction;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { return; }

        other.GetComponent<AliveFlag>().Dead();                  //プレイヤーに当たったらプレイヤーは倒れる
        Destroy(gameObject);                                     //自分も死ぬ
    }
}
