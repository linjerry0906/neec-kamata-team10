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
    private readonly static float MIRROR_Z = 0.0f;                                  //鏡が置ける深度

    [SerializeField]
    private GameObject mirrorUI;
    [SerializeField]
    private GameObject player;                      //プレイヤー
    [SerializeField]
    private GameObject[] mirrors;                   //鏡の種類
    [SerializeField]
    private int maxMirror = 2;                      //最大置ける数
    private int currentMirror;                      //現在選択中の鏡

    private Queue usedMirrors;                      //ステージ上にある鏡
    private ICharacterController controller;        //コントローラー

    private GameObject reflectParent;               //像の親オブジェクト

    void Start ()
    {
        controller = GameManager.Instance.GetController();
        usedMirrors = new Queue();
        reflectParent = new GameObject("Reflects");

        currentMirror = 0;
	}
	
	void Update ()
    {
        if (controller.IsFade())
            return;

        Pause();
        ChangeMirror();                             //鏡の種類を切り替え
        SetMirror();                                //鏡を設置
	}

    private void Pause()
    {
        if (controller.Pause())
            GameManager.Instance.Pause();
    }

    /// <summary>
    /// 鏡の種類を切り替え
    /// </summary>
    private void ChangeMirror()
    {
        int index = currentMirror;                  //Index
        int amount = mirrors.Length;
        if (controller.SwitchToTheLeft())
            index--;
        if (controller.SwitchToTheRight())
            index++;
        if (index == currentMirror)                 //切り替えがない場合
            return;
#region Index Clamp
        if (index >= amount)                        //正数の場合は余りを取る
            index %= amount;
        if(index < 0)                               //負数の場合は余り+最大数（数-左を押した回数）
        {
            index %= amount;
            index += amount;
        }
        currentMirror = index;
        #endregion

        mirrorUI.GetComponent<MirrorSelectPanel>().SetCurrentMirror(currentMirror);
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

        /// <summary>
        /// 6.15 本田 変更部
        /// </summary>

        //CheckMirrorPos(pos);

        //GameObject newMirror = Instantiate(mirrors[currentMirror], pos, Quaternion.identity);   //鏡生成
        //newMirror.GetComponent<Mirror>().SetReflectParent(reflectParent.transform);             //親オブジェクトを設定
        //usedMirrors.Enqueue(newMirror);             //Queueに追加

        if (!IsCollisionToOtherMirror(pos))         //ぶつかっていない場合
        {
            GameObject newMirror = Instantiate(mirrors[currentMirror], pos, Quaternion.identity); //鏡生成
            newMirror.GetComponent<Mirror>().SetReflectParent(reflectParent.transform);           //親オブジェクトを設定
            usedMirrors.Enqueue(newMirror);         //Queueに追加
        }
        /// ここまで

        PlayerAnime();

        CheckQueue();
        RemoveExpiredMirror();                      //多すぎる分を削除
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
    private void CheckMirrorPos(Vector3 pos)
    {
        foreach(GameObject mirror in usedMirrors.ToArray())
        {
            if (!mirror)
                continue;
            Vector3 diff = pos - mirror.transform.position;     //差分
            int diffX = (int)Mathf.Abs(diff.x);                 //正数を取る
            int diffY = (int)Mathf.Abs(diff.y);
            if (diffX < MIRROR_SIZE.x + INTERVAL_MASS &&        //両方範囲内なら置けない
                diffY < MIRROR_SIZE.y + INTERVAL_MASS)
            {
                mirror.GetComponent<Mirror>().DestroyMirror();
            }
        }
    }

    /// <summary>
    /// Queueをチェックする
    /// </summary>
    private void CheckQueue()
    {
        int count = usedMirrors.Count;
        for (int i = 0; i < count; ++i)
        {
            GameObject usedMirror = usedMirrors.Dequeue() as GameObject;
            if (usedMirror == null ||
                !usedMirror.GetComponent<Mirror>().IsAlive())
                continue;

            usedMirrors.Enqueue(usedMirror);
        }
    }

    /// <summary>
    /// 制限数より多い場合は古い順から削除
    /// </summary>
    private void RemoveExpiredMirror()
    {
        if (usedMirrors.Count <= maxMirror)     //少ない場合は何もしない
            return;

        GameObject mirror = usedMirrors.Dequeue() as GameObject;          //削除
        mirror.GetComponent<Mirror>().DestroyMirror();
    }

    /// <summary>
    /// Playerのアニメーションを起動
    /// </summary>
    private void PlayerAnime()
    {
        player.GetComponent<PlayerAnime>().ChangeState(EPlayerState.Action);
    }

    public void SetMirrorUI(GameObject mirrorUI)
    {
        this.mirrorUI = mirrorUI;
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    /// <summary>
    /// 6.15 本田 バグ対策で要望の多かった鏡を消す処理
    /// CheckMirrorPosを改造して実装
    /// 既にある鏡が範囲内か、結果のboolを返すと同時にTrueなら範囲内の鏡を破壊
    /// 
    /// これだと範囲が広すぎるので
    /// PlayerのChildにCollider持たせたりして範囲を狭めたい
    /// </summary>
    private bool IsCollisionToOtherMirror(Vector3 pos)
    {
        foreach (GameObject mirror in usedMirrors.ToArray())
        {
            if (!mirror)
                continue;
            Vector3 diff = pos - mirror.transform.position;     //差分
            int diffX = (int)Mathf.Abs(diff.x);                 //正数を取る
            int diffY = (int)Mathf.Abs(diff.y);
            if (diffX < MIRROR_SIZE.x + INTERVAL_MASS &&        //両方範囲内なら置けない
                diffY < MIRROR_SIZE.y + INTERVAL_MASS)
            {
                mirror.GetComponent<Mirror>().DestroyMirror();  //既存の鏡を破壊
                return true;                                    //衝突状態を返す
            }
        }

        return false;
    }
}
