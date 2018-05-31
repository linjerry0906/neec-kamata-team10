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

    private void Start()
    {
        currentMirror = 0;
        spacing = GetComponent<HorizontalLayoutGroup>().spacing;
        SetCurrentMirror(currentMirror);
    }

    /// <summary>
    /// 選択している鏡をUIに表示
    /// </summary>
    /// <param name="index">添え字</param>
    public void SetCurrentMirror(int index)
    {
        if (index >= mirrors.Length)
            return;

        mirrors[currentMirror].GetComponent<RectTransform>().localScale = new Vector3(MIN_SIZE, MIN_SIZE, 0);   //大きさ
        mirrors[currentMirror].GetComponent<Animator>().SetBool("Selected", false);                             //アニメション

        currentMirror = index;
        mirrors[currentMirror].GetComponent<RectTransform>().localScale = new Vector3(MAX_SIZE, MAX_SIZE, 0);   //大きさ
        mirrors[currentMirror].GetComponent<Animator>().SetBool("Selected", true);                              //アニメション

        GetComponent<RectTransform>().localPosition = START_POS + new Vector3(-spacing * index, 0, 0);
    }
}
