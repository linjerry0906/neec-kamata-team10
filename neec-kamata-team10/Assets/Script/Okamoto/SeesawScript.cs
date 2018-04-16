using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawScript : MonoBehaviour {

    //シーソーに与える力
    [SerializeField]
    private float power = 20.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //他のコライダと接触した時
    void OnControllerColliderHit(ControllerColliderHit col)
    {
        //確認のためレイを視覚的に見えるようにする
        Debug.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * 0.2f, Color.red);

        //rayPositionから下にレイを飛ばし、Blockレイヤーに当たっていたら力を加える
        if (Physics.Linecast(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * 0.2f, LayerMask.GetMask("Seesaw")))
        {
            col.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.down * power, transform.position, ForceMode.Force);
        }
    }
}
