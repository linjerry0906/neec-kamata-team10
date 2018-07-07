using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 6.21 本田　作成 最終更新7.7
/// tag.Equalsがたくさん出てきて見づらいし追加や変更が非常につらい
/// (Switchタグ作ろうとしてどこまで影響出るかわからなくて追加できない)
/// 
/// RigidBodyやColliderを持つObjectのBool管理用で作りました
/// 7月中旬に一斉更新できたらいいかな？
/// </summary>


//考えている使い方

/// <summary>
/// Playerだけ判定する場合とかは元のtag.Equalsでいいけど
/// 複数のTagが絡むResizeとかCollisionの判定は変えたいです(マジな願望)
/// 
/// RigidbodyやColliderと一緒にこいつを持たせて
/// other.GetComponent<BoolList>で渡して判定。
/// BoolListsが無い(Addし忘れた)ときの例外処理は絶対にね
/// </summary>


public class BoolLists : MonoBehaviour {

    private bool isKillingPlayer; //Playerを倒せるObjectか？(MirrorSize関係なく 可能性があれば)
    private bool isKillingEnemy;  //Enemyを倒せるObjectか？(MirrorSize関係なく 可能性があれば)
    private bool isPassable;      //通行可=イベントを発生させるだけのColliderか？
    private bool isResizeable;    //鏡によって変形するか？
    private bool isReflectable;   //鏡に映るか？(変形は関係なく)

    private string objectTag;           //タグ変数
    private bool isUseUpdate;     //タグに応じてUpdateするか？


	void Start () {
        objectTag = GetComponent<GameObject>().tag;   //Objectのtagを取得

        //isUseUpdate = IsUseUpdate();

        bool[] data = BoolManager.GetBool(objectTag);        //tagを渡してデータを取得

        //順番通りに並んでいるのでセット
        isKillingPlayer = data[0]; 
        isKillingEnemy = data[1];  
        isPassable = data[2];      
        isResizeable = data[3];    
        isReflectable = data[4];   
    }

    ///// <summary>
    ///// 必要に応じてbool値を更新したい場合
    ///// IsUseUpdateでtrueを返す
    ///// Updateで個別のUpdateメソッドに飛ばす
    ///// </summary>
    //private bool IsUseUpdate()
    //{
    //    if (objectTag.Equals("Enemy")) return true;
    //    return false;
    //}

    //void Update()
    //{
    //    if (!isUseUpdate) return;
    //}


    /// <summary>
    /// Playerを倒せるObjectか？(MirrorSize関係なく 可能性があれば)
    /// </summary>
    public bool IsKillingPlayer { get { return isKillingPlayer; } }

    /// <summary>
    /// Enemyを倒せるObjectか？(MirrorSize関係なく 可能性があれば)
    /// </summary>
    public bool IsKillingEnemy { get { return isKillingEnemy; } }

    /// <summary>
    /// 通行可=イベントを発生させるだけのCollider？
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
