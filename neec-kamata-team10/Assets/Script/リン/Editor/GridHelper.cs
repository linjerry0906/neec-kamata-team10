//------------------------------------------------------
// 作成日：2018.4.3
// 作成者：林 佳叡
// 内容：グリッド上に置く
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper{

    /// <summary>
    /// グリッドに収めるように調整
    /// </summary>
    /// <param name="pos">オブジェクトの位置</param>
    /// <returns></returns>
	public static Vector3 SetOnGrid(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        return new Vector3(x + 0.5f, y + 0.5f, pos.z);
    }
}
