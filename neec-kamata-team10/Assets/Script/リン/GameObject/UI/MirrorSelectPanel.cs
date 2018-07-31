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
    private int prevMirror;
    [SerializeField]
    private float animTime = 0.2f;  //アニメーションさせる時間
    private Timer timer;
    [SerializeField]
    private GameObject fakeFirst;
    [SerializeField]
    private GameObject fakeLast;

    private Vector3 MinSizeUI, MaxSizeUI;

    private PlayerUI playerUI;


    private void Start()
    {
        currentMirror = 0;
        spacing = GetComponent<HorizontalLayoutGroup>().spacing;

        timer = new Timer(animTime);
        MinSizeUI = new Vector3(MIN_SIZE, MIN_SIZE, 0);
        MaxSizeUI = new Vector3(MAX_SIZE, MAX_SIZE, 0);

        playerUI = GameObject.Find("Player").GetComponentInChildren<PlayerUI>();
        SetCurrentMirror(currentMirror);
    }

    private void Update()
    {
        if (timer.IsTime()) return;

        timer.TimerUpdate();
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

        timer.Initialize();
  
        mirrors[currentMirror].GetComponent<Animator>().SetBool("Selected", false);                             //アニメション
        prevMirror = currentMirror;
        currentMirror = index;
        mirrors[currentMirror].GetComponent<Animator>().SetBool("Selected", true);                              //アニメション


        playerUI.ChangeMirrorUI(currentMirror);
    }

    /// <summary>
    /// アニメーション中のUIのサイズを設定
    /// </summary>
    private void SetUISize()
    {
        int prevIndex = prevMirror;
        if(prevMirror - currentMirror > 1)
        {
            fakeFirst.GetComponent<RectTransform>().localScale = Vector3.Lerp(MaxSizeUI, MinSizeUI, timer.Rate());              //古いものは小さくする方向
            prevIndex = -1;
        }
        if(prevMirror - currentMirror < -1)
        {
            fakeLast.GetComponent<RectTransform>().localScale = Vector3.Lerp(MaxSizeUI, MinSizeUI, timer.Rate());               //古いものは小さくする方向
            prevIndex = mirrors.Length;
        }

        mirrors[prevMirror].GetComponent<RectTransform>().localScale = Vector3.Lerp(MaxSizeUI, MinSizeUI, timer.Rate());        //古いものは小さくする方向
        mirrors[currentMirror].GetComponent<RectTransform>().localScale = Vector3.Lerp(MinSizeUI, MaxSizeUI, timer.Rate());     //新しいものは大きく

        GetComponent<RectTransform>().localPosition = START_POS + Vector3.Lerp(
            new Vector3(-spacing * prevIndex, 0, 0),
            new Vector3(-spacing * currentMirror, 0, 0),
            timer.Rate());
    }
}
