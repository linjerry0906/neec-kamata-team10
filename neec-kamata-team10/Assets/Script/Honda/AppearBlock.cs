using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作成日   2018/6/9
/// 最終更新 2018/6/11
/// 作成者   本田尚大
/// 
/// 使い方
///     Objectの Active = false にするとコードまで無効になるので
///     Emptyにこのコードを持たせてEmptyのChildとして対象のObjectを持たせてください。
///     UnityのGameObject.Activeを切り替えているのでなんでも操作できるはず。
/// </summary>

public class AppearBlock : MonoBehaviour
{
    public bool IsReverseAppear = false; //trueだとスイッチOnで消滅、スイッチOffで出現

    [SerializeField]
    public SwitchObject switchObj; //スイッチ本体(のコード)

    public GameObject appearObject; //スイッチで切り替えさせるObject

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

        appearObject.SetActive(switchObj.IsTurnOn ^ IsReverseAppear);
	}
}
