using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 6.21 本田　作成
/// tag.Equalsがたくさん出てきて見づらいし追加や変更が非常につらい
/// (Switchタグ作ろうとしてどこまで影響出るかわからなくて追加できない)
/// Bool管理用で作りました　7月中旬に一斉更新できたらいいかな？
/// 
/// BoolListsが無い(Addし忘れた)ときの例外処理は絶対にね
/// </summary>

public class BoolLists : MonoBehaviour {

    private bool isKillingPlayer; //Playerを倒せるObjectか？(全MirrorSizeでのOR条件)
    private bool isKillingEnemy;  //Enemyを倒せるObjectか？(全MirrorSizeでのOR条件)
    private bool isPassable;      //通行可=イベントを発生させるだけのColliderか？
    private bool isResizeable;    //鏡によって変形するか？
    private bool isReflectable;   //鏡に映るか？(変形は関係なく)


	void Start () {
        string tag = GetComponent<GameObject>().tag;   //Objectのtagを取得

        bool[] data = BoolManager.GetBool(tag);        //tagを渡してデータを取得

        //順番通りに並んでいるのでセット
        isKillingPlayer = data[0]; 
        isKillingEnemy = data[1];  
        isPassable = data[2];      
        isResizeable = data[3];    
        isReflectable = data[4];   
    }

    /// <summary>
    /// Playerを倒せるObjectか？(全MirrorSizeでのOR条件)
    /// </summary>
    public bool IsKillingPlayer { get { return isKillingPlayer; } }

    /// <summary>
    /// Enemyを倒せるObjectか？(全MirrorSizeでのOR条件)
    /// </summary>
    public bool IsKillingEnemy { get { return isKillingEnemy; } }

    /// <summary>
    /// 通行可=イベントを発生させるだけのColliderか？
    /// </summary>
    public bool IsPassable { get { return isPassable; } }

    /// <summary>
    /// 鏡によって変形するか？
    /// </summary>
    public bool IsResizeable { get { return isResizeable; } }

    /// <summary>
    /// 鏡に映るか？(変形は関係なく)
    /// </summary>
    public bool IsReflectable { get { return isReflectable; } }
}
