using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawScript : MonoBehaviour {

    private float maxAngle = 15;
    private float minAngle = -15;

    //回転速度と前の回転角
    private float rotateSpeed = 0f;
    private float lastRotate;
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

        //回転の計算
        CalcRoteteSpeed();
    }

    //トリガー側から取得
    void OnTriggerStay(Collider c)
    {
        //Debug.Log("hit");
        //Rigidbody cRigidBody = c.gameObject.GetComponent<Rigidbody>();
        //if (cRigidBody == null) { return; }
        //Vector3 velocity = cRigidBody.velocity;
        //velocity.y = 0;
        // cRigidBody.velocity = velocity;

        //Player以外はサヨナラ 回転速度が遅くても無視
        if (c.tag.Equals("Player") && Mathf.Abs(rotateSpeed) > 0.1f)
        {
            //座標を取得
            Vector3 playerPos = c.transform.position;
            Vector3 seesawPos = this.transform.position;

            //長さを計算
            float length = Vector3.Distance(playerPos, seesawPos);

            //角速度をそのまま突っ込む
            Rigidbody pBody = c.GetComponent<Rigidbody>();
            pBody.AddForce(Vector3.up * length * Mathf.Abs(rotateSpeed));

            Debug.Log("Forced");
        }
    }

    //速度計算
    void CalcRoteteSpeed()
    {
        rotateSpeed = Mathf.Deg2Rad * 1f * (transform.eulerAngles.z - lastRotate) / Time.deltaTime;
        lastRotate = transform.eulerAngles.z;

        if (rotateSpeed > Mathf.PI) rotateSpeed -= Mathf.PI;
        else if (rotateSpeed < -Mathf.PI) rotateSpeed += Mathf.PI;
    }
}
