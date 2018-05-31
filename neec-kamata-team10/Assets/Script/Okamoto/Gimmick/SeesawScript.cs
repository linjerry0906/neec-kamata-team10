using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawScript : MonoBehaviour {

    private float maxAngle = 15;
    private float minAngle = -15;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update(){
    }

    void FixedUpdate()
    {
        //現在の回転角度を0～360から-180～180に変換
        float rotateZ = (transform.eulerAngles.z > 180) ? transform.eulerAngles.z - 360 : transform.eulerAngles.z;
        //現在の回転角度をMathf.Clamp()を使ってminAngleからMaxAngle内に収まるようにする
        float angleZ = Mathf.Clamp(rotateZ, minAngle, maxAngle);
        //回転角度を0～360に戻す
        angleZ = (angleZ < 0) ? angleZ + 360 : angleZ;
        //回転角度をオブジェクトに適用
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }

    void OnCollisionStay(Collision c)
    {
        //Debug.Log("hit");
        //Rigidbody cRigidBody = c.gameObject.GetComponent<Rigidbody>();
        //if (cRigidBody == null) { return; }
        //Vector3 velocity = cRigidBody.velocity;
        //velocity.y = 0;
        // cRigidBody.velocity = velocity;
    }
}
