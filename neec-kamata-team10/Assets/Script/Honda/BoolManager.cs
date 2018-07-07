using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 6.21 本田作成 最終更新 7.7
/// BoolListsで使うboolの定義Class
/// </summary>

public static class BoolManager{

    /// <summary>
    /// タグからBoolを取得する入り口Method
    /// ここだけはどうしようもない...
    /// </summary>
    public static bool[] GetBool(string objectTag)
    {
        if (objectTag.Equals("template")) return template;       //ただのテンプレートです。コピペして定義してください。

        if (objectTag.Equals("stage_block")                          //ステージの床
            || objectTag.Equals("appear_block")                      //AppearBlock(出現中)
            || objectTag.Equals("seasaw")) return stageBlock;        //シーソー(この処理は共通)

        if (objectTag.Equals("Player")) return player;               //Player

        if (objectTag.Equals("Enemy")) return enemy;                 //Simple,ChaseEnemy

        if (objectTag.Equals("magic_block")) return magicBlock;      //magicBlock

        if (objectTag.Equals("Splinter")) return splinter;           //トゲ

        if (objectTag.Equals("Respawn")                              //リスポ
            || objectTag.Equals("tutorialMessage")                   //チュートリアル
            || objectTag.Equals("Switch")) return eventObject;       //スイッチ

        //統合して定義したい軍団
        if (objectTag.Equals("ResizeCollider")) return judgeCollider_Resize;  //判定用の見えなくて通過するColliderのうち、変形するもの
        if (objectTag.Equals("UnresizeCollider")) return judgeCollider_block; //変形しないもの

        return stageBlock;
    }

    //テンプレート コピペしましょう。
    private static readonly bool[] template = new bool[]
    {
        true, //isKillingPlayer
        true, //isKillingEnemy
        true, //isPassable
        true, //isResizeable
        true  //isReflectable
    };

    private static readonly bool[] stageBlock = new bool[]
{
        false, //isKillingPlayer
        false, //isKillingEnemy
        false, //isPassable
        false, //isResizeable
        true  //isReflectable
};

    private static readonly bool[] enemy = new bool[]
{
        true, //isKillingPlayer
        false, //isKillingEnemy
        false, //isPassable
        true, //isResizeable
        true  //isReflectable
};

    private static readonly bool[] player = new bool[]
{
        false, //isKillingPlayer
        true, //isKillingEnemy
        false, //isPassable
        false, //isResizeable
        true  //isReflectable
};

    private static readonly bool[] magicBlock = new bool[]
    {
        false, //isKillingPlayer
        false, //isKillingEnemy
        false, //isPassable
        true, //isResizeable
        true  //isReflectable
    };

    private static readonly bool[] splinter = new bool[]
{
        true, //isKillingPlayer
        true, //isKillingEnemy
        false, //isPassable
        true, //isResizeable
        true  //isReflectable
};

    //RespawnとTutorialMessageとSwitch この部分は共通
    private static readonly bool[] eventObject = new bool[]
    {
        false, //isKillingPlayer
        false, //isKillingEnemy
        true, //isPassable
        false, //isResizeable
        true  //isReflectable
    };

    //透明なCollider
    private static readonly bool[] judgeCollider_block = new bool[]
    {
        false, //isKillingPlayer
        false, //isKillingEnemy
        true, //isPassable
        false, //isResizeable
        false  //isReflectable
    };

    private static readonly bool[] judgeCollider_Resize = new bool[]
    {
        false, //isKillingPlayer
        false, //isKillingEnemy
        true, //isPassable
        true, //isResizeable
        false  //isReflectable
    };
}
