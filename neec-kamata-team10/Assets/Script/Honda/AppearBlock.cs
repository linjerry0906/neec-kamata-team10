using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearBlock : MonoBehaviour
{
    public bool IsReverseAppear = false; //trueだとスイッチOnで消滅、スイッチOffで出現

    [SerializeField]
    public SwitchObject switchObj;

    public new GameObject gameObject;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        ///<summary>
        /// アクティブ状態をスイッチから判断
        /// Activeの条件は
        /// 
        ///                 |  switch-On?
        ///      IsReverse? | false | true
        /// ----------------+-------+-------
        ///  normal = false | false | true
        /// reverse =  true | true  | false
        /// 
        /// なのでXORで設定できる
        ///</summary>

        gameObject.SetActive(switchObj.IsTurnOn ^ IsReverseAppear);
	}
}
