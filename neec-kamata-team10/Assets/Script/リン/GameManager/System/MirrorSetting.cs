//------------------------------------------------------
// 作成日：2018.4.25
// 作成者：林 佳叡
// 内容：鏡を設置する機能
//------------------------------------------------------
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MirrorSetting : MonoBehaviour
{
    private readonly static Vector2Int MIRROR_SIZE = new Vector2Int(5, 4);          //鏡のサイズ
    private readonly static int INTERVAL_MASS = 2;                                  //おける間隔
    private readonly static float MIRROR_Z = 0.1f;                                  //鏡が置ける深度

    [SerializeField]
    private GameObject player;                      //プレイヤー
    [SerializeField]
    private GameObject[] mirrors;                   //鏡の種類
    [SerializeField]
    private static int maxMirror = 3;               //最大置ける数
    private int currentMirror;                      //現在選択中の鏡
    private bool onHand;                            //手に持っているか
    private GameObject handMirror = null;           //手に持っている鏡

    private Queue usedMirrors;                      //ステージ上にある鏡
    private ICharacterController controller;        //コントローラー

    private GameObject reflectParent;               //像の親オブジェクト

    void Start ()
    {
        controller = GameManager.Instance.GetController();
        usedMirrors = new Queue();
        reflectParent = new GameObject("Reflects");

        currentMirror = 0;
        onHand = false;
	}
	
	void Update ()
    {
        ChangeMirror();                             //鏡の種類を切り替え
        SetMirror();                                //鏡を設置

        if (onHand)
            handMirror.GetComponent<MirrorOnHand>().UpdateHand();       //位置更新
	}

    /// <summary>
    /// 鏡の種類を切り替え
    /// </summary>
    private void ChangeMirror()
    {
        if (onHand)                                 //手に持っていれば変えられない
            return;

        int amount = mirrors.Length;
        if (controller.SwitchToTheLeft())
            currentMirror--;
        if (controller.SwitchToTheRight())
            currentMirror++;
#region Index Clamp
        if (currentMirror >= amount)                //正数の場合は余りを取る
            currentMirror %= amount;
        if(currentMirror < 0)                       //負数の場合は余り+最大数（数-左を押した回数）
        {
            currentMirror %= amount;
            currentMirror += amount;
        }
#endregion
    }

    /// <summary>
    /// 鏡を設置
    /// </summary>
    private void SetMirror()
    {
        if (!controller.OperateTheMirror())         //設置ボタンを押してなかったら何もしない
            return;

        Vector3 pos = MirrorPos();                  //設置位置を計算
        ClampGrid(ref pos);                         //グリッド上に設定

        if (!CheckMirrorPos(pos))                   //この位置に置けるかを確認
        {
            onHand = true;                          //手に持つ
            handMirror.AddComponent<MirrorOnHand>();                                            //コンポーネント追加
            handMirror.GetComponent<MirrorOnHand>().SetPlayer(player);
            handMirror.GetComponent<Mirror>().SetHand(onHand);
            PlayerAnime();
            return;
        }

        if (onHand)                                 //手に持っていれば
        {
            SetHandMirror(pos);                     //鏡を設置
            PlayerAnime();
            return;
        }

        GameObject newMirror = Instantiate(mirrors[currentMirror], pos, Quaternion.identity);   //鏡生成
        newMirror.GetComponent<Mirror>().SetReflectParent(reflectParent.transform);             //親オブジェクトを設定
        usedMirrors.Enqueue(newMirror);             //Queueに追加
        PlayerAnime();

        RemoveExpiredMirror();                      //多すぎる分を削除
        SetColor();                                 //色設定
    }

    /// <summary>
    /// 手に持っている鏡を設置
    /// </summary>
    private void SetHandMirror(Vector3 pos)
    {
        Destroy(handMirror.GetComponent<MirrorOnHand>());       //移動用のコンポーネントを削除
        onHand = false;
        handMirror.GetComponent<Mirror>().SetHand(onHand);
        handMirror.transform.position = pos;                    //位置設定
        handMirror.GetComponent<Mirror>().Release();
        handMirror = null;
    }

    /// <summary>
    /// 鏡の位置を計算
    /// </summary>
    /// <returns></returns>
    private Vector3 MirrorPos()
    {
        EDirection d = player.GetComponent<Player>().GetDirection();        //向き
        int interval = MIRROR_SIZE.x / 2 + 2;                               //プレイヤーとの間隔
        if (d == EDirection.LEFT)                                           //左の場合
            interval *= -1;
        return player.transform.position + new Vector3(interval, 0, 0);     //位置+間隔
    }

    /// <summary>
    /// グリッド上に設定
    /// </summary>
    /// <param name="pos">位置</param>
    private void ClampGrid(ref Vector3 pos)
    {
        pos.x = Mathf.FloorToInt(pos.x) + 0.5f;
        pos.y = Mathf.FloorToInt(pos.y) + MIRROR_SIZE.y / 2;
        pos.z = MIRROR_Z;                                       //深度設定
    }

    /// <summary>
    /// 置けるかを確認
    /// </summary>
    /// <param name="newMirror">新しい鏡</param>
    /// <returns></returns>
    private bool CheckMirrorPos(Vector3 pos)
    {
        foreach(GameObject mirror in usedMirrors.ToArray())
        {
            if (handMirror == mirror)
                continue;
            Vector3 diff = pos - mirror.transform.position;     //差分
            int diffX = (int)Mathf.Abs(diff.x);                 //正数を取る
            int diffY = (int)Mathf.Abs(diff.y);
            if (diffX < MIRROR_SIZE.x + INTERVAL_MASS &&        //両方範囲内なら置けない
                diffY < MIRROR_SIZE.y + INTERVAL_MASS)
            {
                if(!onHand)                                     //持っていなければ持つ
                    handMirror = mirror;                        //持つ鏡を設定
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 制限数より多い場合は古い順から削除
    /// </summary>
    private void RemoveExpiredMirror()
    {
        if (usedMirrors.Count <= maxMirror)     //少ない場合は何もしない
            return;

        Destroy(usedMirrors.Dequeue() as GameObject);          //削除
    }

    /// <summary>
    /// 色設定
    /// </summary>
    private void SetColor()
    {
        Color[] colors = new Color[3];
        colors[0] = new Color(1.0f, 0.0f, 0.0f, 0.2f);
        colors[1] = new Color(1.0f, 1.0f, 0.0f, 0.2f);
        colors[2] = new Color(0.0f, 0.0f, 1.0f, 0.2f);
        int index = 0;

        foreach (GameObject mirror in usedMirrors.ToArray())
        {
            MeshRenderer mesh = mirror.transform.GetChild(5).GetComponent<MeshRenderer>();
            mesh.material.color = colors[index];
            ++index;
        }
    }

    /// <summary>
    /// Playerのアニメーションを起動
    /// </summary>
    private void PlayerAnime()
    {
        player.GetComponent<PlayerAnime>().ChangeState(EPlayerState.Action);
    }
}
