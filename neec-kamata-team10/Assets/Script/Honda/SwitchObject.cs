using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作成日 2018/6/9
/// 作成者 本田 尚大
/// 
/// コメント:
///   1flame待機で試してみたらかなりジャッジが不安定だったから
///   安定させる為に多重フレーム判定を入れたかっただけなのに
///   なんでbyteにしてこんな構文書いたんだろ
///   
///   やってること
///   byte型をboolが8個の配列に見立てて
///   上の桁ほど古いデータ、下ほど新しいデータと設定
/// </summary>

public class SwitchObject : MonoBehaviour {

    public bool IsTurnOn = false;  //Switchの起動状態bool
    private byte Judge;            //動作安定用の8flame待機bool

    //// Use this for initialization
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        ////ジャッジを反映 8flameのどこかでも触れた形跡があったらTrue
        //IsTurnOn = (Judge != 0);

        /// <summary>
        /// ラグが気になるならこちらで設定
        /// 
        /// 対象flame数 1 2 3  4  5  6   7   8
        /// 設定する値   1 3 7 15 31 63 127 255 
        /// 
        /// </summary>
        IsTurnOn = ((Judge & 7) != 0);

        //これからの1flameのジャッジのを入れるために左に1bitシフト
        Judge = (byte)(Judge << 1);
    }

    void OnTriggerStay(Collider other)
    {
        //新たに対象Targetとの接触があれば直前flameに情報を入れる 但し既に入っていたら無視したいのでOR演算
        if (IsColliderTarget(other.tag))
        {
            Judge = (byte)(Judge | 1);
        }
    }

    public bool IsColliderTarget(string tag) //ぶつかっているColliderがtargetかどうか返す
    {
        //現状のSwitch対象はPlayerタグとEnemyタグのみ
        if (tag.Equals("Player")) return true;
        else if (tag.Equals("Enemy")) return true;

        //else
        return false;
    }
}
