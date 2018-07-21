//------------------------------------------------------
// 作成日：2018.5.25
// 作成者：林 佳叡
// 内容：UI 鏡選択を表示
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorSelectPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mirrors;

    private static readonly Vector3 START_POS = new Vector3(245, 0, 0);
    [SerializeField]
    private int MAX_SIZE = 55;      //最大サイズ
    [SerializeField]
    private int MIN_SIZE = 30;      //最小サイズ
    private float spacing;

    private int currentMirror = 0;


    /// <summary>
    /// 7.21 本田
    /// アニメーション用の追加Value
    /// </summary>
    private int oldUI;             //UIのindex 古い番号
    private int newUI;             //新しい番号
    [SerializeField]
    private float animTime = 0.2f; //アニメーションさせる時間
    private float timeNow;         //timeカウンタ
    [SerializeField]
    private GameObject fakeFirst;  //fake
    [SerializeField]
    private GameObject fakeLast;   //fake

    private Vector3 MinSizeUI, MaxSizeUI; //UIのサイズを先に変数定義したい


    private void Start()
    {
        currentMirror = 0;
        spacing = GetComponent<HorizontalLayoutGroup>().spacing;
        SetCurrentMirror(currentMirror);

        timeNow = animTime;        //アニメーション終了してるように
        MinSizeUI = new Vector3(MIN_SIZE, MIN_SIZE, 0);
        MaxSizeUI = new Vector3(MAX_SIZE, MAX_SIZE, 0);
        SetCurrentMirror(0);
    }

    /// <summary>
    /// 7.21本田 切り替え時のサイズ変更やUI移動がアニメーションするようにしてみた
    /// </summary>
    /// 
    private void Update()
    {
        if (timeNow >= animTime) return;

        timeNow += Time.deltaTime;
        SetUISize();
    }

    /// <summary>
    /// 選択している鏡をUIに表示
    /// </summary>
    /// <param name="index">添え字</param>
    public void SetCurrentMirror(int index)
    {
        if (index >= mirrors.Length)
            return;

        #region 切り替えアニメーションしないVersion
        //mirrors[currentMirror].GetComponent<RectTransform>().localScale = new Vector3(MIN_SIZE, MIN_SIZE, 0);   //大きさ
        //mirrors[currentMirror].GetComponent<Animator>().SetBool("Selected", false);                             //アニメション

        //currentMirror = index;
        //mirrors[currentMirror].GetComponent<RectTransform>().localScale = new Vector3(MAX_SIZE, MAX_SIZE, 0);   //大きさ
        //mirrors[currentMirror].GetComponent<Animator>().SetBool("Selected", true);                              //アニメション

        //GetComponent<RectTransform>().localPosition = START_POS + new Vector3(-spacing * index, 0, 0);
        #endregion

        #region 切り替えアニメーションするVersion

        timeNow = 0f;          //時間リセット

        oldUI = currentMirror; //古い情報を保存     
        mirrors[currentMirror].GetComponent<Animator>().SetBool("Selected", false);                             //アニメション

        //端から端の例外設定 5->0の場合
        if(index - currentMirror < -1)
        {
            mirrors[oldUI].GetComponent<RectTransform>().localScale = MinSizeUI;   //先に本物の表示を戻す
            oldUI = -1;
        }
        //例外設定 0->5
        else if(index - currentMirror > 1)
        {
            mirrors[oldUI].GetComponent<RectTransform>().localScale = MinSizeUI;   //先に本物の表示を戻す
            oldUI = mirrors.Length;
        }

        currentMirror = index;
        newUI = index;
        mirrors[currentMirror].GetComponent<Animator>().SetBool("Selected", true);                              //アニメション
#endregion
    }

    /// <summary>
    /// アニメーション中のUIのサイズを設定
    /// </summary>
    private void SetUISize()
    {
        if(oldUI < 0) //マイナス側ループ処理
        {
            fakeFirst.GetComponent<RectTransform>().localScale = Vector3.Lerp(MaxSizeUI, MinSizeUI, timeNow / animTime);   //古いものは小さくする方向
        }
        else if(oldUI >= mirrors.Length) //プラス側ループ処理
        {
            fakeLast.GetComponent<RectTransform>().localScale = Vector3.Lerp(MaxSizeUI, MinSizeUI, timeNow / animTime);   //古いものは小さくする方向
        }
        else //範囲内なら
        {
            mirrors[oldUI].GetComponent<RectTransform>().localScale = Vector3.Lerp(MaxSizeUI, MinSizeUI, timeNow / animTime);   //古いものは小さくする方向
        }

        mirrors[newUI].GetComponent<RectTransform>().localScale = Vector3.Lerp(MinSizeUI, MaxSizeUI, timeNow / animTime);   //新しいものは大きく

        GetComponent<RectTransform>().localPosition = START_POS + Vector3.Lerp(
            new Vector3(-spacing * oldUI, 0, 0),
            new Vector3(-spacing * newUI, 0, 0),
            timeNow / animTime);
    }
}
