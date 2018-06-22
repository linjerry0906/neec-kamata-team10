using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 6.21 本田作成
/// BoolListsで使うboolの定義Class
/// </summary>

public static class BoolManager{

    /// <summary>
    /// タグからBoolを取得する入り口Method
    /// ここだけはどうしようもない...
    /// </summary>
    public static bool[] GetBool(string tag)
    {
        if (tag.Equals("template")) return template;
        if (tag.Equals("stage_block")) return stageBlock;
        if (tag.Equals("Player")) return player;
        if (tag.Equals("Enemy")) return enemy;

        return stageBlock;
    }


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
}
