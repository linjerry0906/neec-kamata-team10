//------------------------------------------------------
// 作成日：2018.5.28
// 作成者：林 佳叡
// 内容：結果のパネル
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{

    [SerializeField]
    private GameObject clearObj;            //clearを表示するオブジェクト
    [SerializeField]
    private GameObject failedObj;           //failを表示するオブジェクト

    [SerializeField]
    private float maxAlpha;                 //最大透明値
    [SerializeField]
    private float alphaChangeValue;         //透明値変化量
    private Color panelColor;               //パネルの色

    private GameObject resultObj;

	void Start ()
    {
        panelColor = GetComponent<Image>().color;
        panelColor.a = 0;
        GetComponent<Image>().color = panelColor;
    }
	
	void Update ()
    {
        Color resultColor = resultObj.GetComponent<Image>().color;
        if (resultColor.a < 1)
        {
            resultColor.a += alphaChangeValue;
            resultObj.GetComponent<Image>().color = resultColor;
        }

        if (panelColor.a < maxAlpha)
        {
            panelColor.a += alphaChangeValue;
            GetComponent<Image>().color = panelColor;
            return;
        }
	}

    /// <summary>
    /// クリアしたかを設定
    /// </summary>
    /// <param name="isClear">クリアしたか</param>
    public void SetIsClear(bool isClear)
    {
        if (isClear)
        {
            SetObjActive(ref clearObj, ref failedObj);
            return;
        }

        SetObjActive(ref failedObj, ref clearObj);
    }

    /// <summary>
    /// Switch機能
    /// </summary>
    /// <param name="on"></param>
    /// <param name="off"></param>
    private void SetObjActive(ref GameObject on, ref GameObject off)
    {
        on.SetActive(true);
        off.SetActive(false);
        resultObj = on;

        Color resultColor = resultObj.GetComponent<Image>().color;
        resultColor.a = 0;
        resultObj.GetComponent<Image>().color = resultColor;
    }
}
