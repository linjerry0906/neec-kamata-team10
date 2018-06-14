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
        if (!isClear)
        {
            GameManager.Instance.TrySameStage(isClear);

            //6.15 本田追記 カメラを高速移動モードに更新
            CameraWork work = Camera.main.GetComponent<CameraWork>();
            Debug.Log(work != null);
            work.SetRespawnTrace();
            //ここまで
            return;
        }

        GameObject panel = Instantiate(resultPanel);
        panel.transform.GetChild(0).GetComponent<ResultUI>().SetIsClear(isClear);
    }
}
