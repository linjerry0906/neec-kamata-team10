//------------------------------------------------------
// 作成日：2018.5.28
// 作成者：林 佳叡
// 内容：ゲーム結果を管理するマネージャー
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private GameObject resultPanel;

    /// <summary>
    /// ゲーム終了
    /// </summary>
    /// <param name="isClear">クリアしたか</param>
    public void GameOver(bool isClear)
    {
        GameObject panel = Instantiate(resultPanel);
        panel.transform.GetChild(0).GetComponent<ResultUI>().SetIsClear(isClear);
    }
}
